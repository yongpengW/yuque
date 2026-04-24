import { useQuery } from '@tanstack/react-query';
import { Button } from 'antd';
import { MoreHorizontal, Share2, Star } from 'lucide-react';
import { Link, useParams } from 'react-router-dom';
import { getRepository } from '../../services/knowledgeService';
import styles from './RepositoryPage.module.css';

export function RepositoryPage() {
  const { repoKey = '' } = useParams();
  const { data: repo } = useQuery({ queryKey: ['repository', repoKey], queryFn: () => getRepository(repoKey) });

  if (!repo) {
    return <div className={styles.empty}>知识库不存在</div>;
  }

  return (
    <article className={styles.page}>
      <header className={styles.hero}>
        <div className={styles.titleRow}>
          <span className={styles.bookIcon} />
          <div>
            <h1>{repo.name}</h1>
            <p>
              <strong>{repo.documentCount}</strong> 文档　 <strong>{repo.wordCount}</strong> 字
            </p>
          </div>
          <div className={styles.actions}>
            <Button icon={<Star size={18} />}>已收藏</Button>
            <Button icon={<Share2 size={18} />}>分享</Button>
            <Button icon={<MoreHorizontal size={18} />} />
          </div>
        </div>
        <section className={styles.welcome}>
          <strong>欢迎来到知识库</strong>
          <p>{repo.description}</p>
        </section>
      </header>
      <section className={styles.catalog}>
        {repo.catalogGroups.map((group) => (
          <div key={group.id} className={styles.group}>
            <h2>{group.title}</h2>
            {group.documents.map((doc) => (
              <Link key={doc.key} to={`/r/${repo.key}/d/${doc.key}`} className={styles.row}>
                <span>{doc.title}</span>
                <time>{doc.updatedAt}</time>
              </Link>
            ))}
          </div>
        ))}
      </section>
    </article>
  );
}
