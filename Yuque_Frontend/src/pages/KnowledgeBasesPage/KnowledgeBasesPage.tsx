import { useQuery } from '@tanstack/react-query';
import { Button, Dropdown, Segmented } from 'antd';
import { Grid3X3, List, Plus } from 'lucide-react';
import { Link } from 'react-router-dom';
import { EntityIcon } from '../../components/EntityIcon/EntityIcon';
import { getKnowledgeBases } from '../../services/knowledgeService';
import styles from './KnowledgeBasesPage.module.css';

export function KnowledgeBasesPage() {
  const { data: repos = [] } = useQuery({ queryKey: ['knowledgeBases'], queryFn: getKnowledgeBases });

  return (
    <main className={styles.page}>
      <h1>知识库</h1>
      <section className={styles.common}>
        <header>
          <strong>常用</strong>
          <button>收起⌃</button>
        </header>
        {repos.map((repo) => (
          <Link key={repo.key} to={`/r/${repo.key}`} className={styles.commonCard}>
            <EntityIcon type="repository" size="md" />
            <strong>{repo.name}</strong>
            {repo.isPrivate ? <span>锁</span> : null}
          </Link>
        ))}
      </section>

      <section className={styles.mine}>
        <div className={styles.toolbar}>
          <Segmented options={['我个人的', '邀请协作的']} defaultValue="我个人的" />
          <div className={styles.actions}>
            <Dropdown menu={{ items: [{ key: 'repo', label: '新建知识库' }, { key: 'group', label: '新建分组' }] }}>
              <Button type="primary" ghost icon={<Plus size={16} />} />
            </Dropdown>
            <Button icon={<Grid3X3 size={16} />} />
            <Button icon={<List size={16} />} />
          </div>
        </div>
        <h2>我的知识库</h2>
        <div className={styles.repoGrid}>
          {repos.map((repo) => (
            <Link key={repo.key} to={`/r/${repo.key}`} className={styles.repoCard}>
              <div>
                <EntityIcon type="repository" size="md" />
                <strong>{repo.name}</strong>
              </div>
              <ul>
                {repo.pinnedDocuments.map((doc) => (
                  <li key={doc.id}>
                    <span>{doc.title}</span>
                    <time>{doc.updatedAt}</time>
                  </li>
                ))}
              </ul>
            </Link>
          ))}
        </div>
      </section>
    </main>
  );
}
