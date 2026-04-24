import { useQuery } from '@tanstack/react-query';
import { Button, Dropdown, Segmented } from 'antd';
import { Bot, ChevronDown, FilePlus2, LibraryBig, Palette, PlusCircle } from 'lucide-react';
import { Link } from 'react-router-dom';
import { getRecentDocuments } from '../../services/knowledgeService';
import styles from './HomePage.module.css';

const quickActions = [
  { label: '新建文档', sub: '文档、表格、画板、数据表', icon: FilePlus2 },
  { label: '新建知识库', sub: '使用知识库整理知识', icon: LibraryBig },
  { label: '模板中心', sub: '从模板中获取灵感', icon: Palette },
  { label: 'AI 帮你写', sub: 'AI 助手帮你一键生成文档', icon: Bot },
];

export function HomePage() {
  const { data: documents = [] } = useQuery({ queryKey: ['recentDocuments'], queryFn: getRecentDocuments });

  return (
    <main className={styles.page}>
      <h1>开始</h1>
      <section className={styles.quickGrid}>
        {quickActions.map((action) => (
          <button key={action.label} className={styles.quickCard}>
            <action.icon size={28} />
            <span>
              <strong>{action.label}</strong>
              <small>{action.sub}</small>
            </span>
            {action.label === '新建文档' ? <ChevronDown size={18} /> : null}
          </button>
        ))}
      </section>

      <section className={styles.documents}>
        <div className={styles.sectionHeader}>
          <h2>文档</h2>
          <div className={styles.filters}>
            <Button type="text">类型⌄</Button>
            <Button type="text">归属⌄</Button>
            <Button type="text">创建者⌄</Button>
          </div>
        </div>
        <Segmented options={['编辑过', '浏览过', '我点赞的', '我评论过']} defaultValue="编辑过" />
        <div className={styles.docList}>
          {documents.map((doc) => (
            <Link key={doc.id} to={`/r/${doc.repositoryKey}/d/${doc.key}`} className={styles.docRow}>
              <span className={styles.docIcon} />
              <strong>{doc.title}</strong>
              <span>{doc.owner} / {doc.repositoryName}</span>
              <time>{doc.updatedAt}</time>
            </Link>
          ))}
        </div>
      </section>
      <Dropdown menu={{ items: [{ key: 'doc', label: '新建文档' }, { key: 'repo', label: '新建知识库' }] }}>
        <Button className={styles.floatingCreate} icon={<PlusCircle size={22} />} />
      </Dropdown>
    </main>
  );
}
