import { useQuery } from '@tanstack/react-query';
import { Button } from 'antd';
import { Edit3, MessageSquare, MoreHorizontal, Share2, Star } from 'lucide-react';
import { useParams } from 'react-router-dom';
import { getDocument } from '../../services/knowledgeService';
import styles from './DocumentPage.module.css';

export function DocumentPage() {
  const { repoKey = '', docKey = '' } = useParams();
  const { data: document } = useQuery({
    queryKey: ['document', repoKey, docKey],
    queryFn: () => getDocument(repoKey, docKey),
  });

  if (!document) {
    return <div className={styles.page}>文档不存在</div>;
  }

  return (
    <article className={styles.page}>
      <header className={styles.header}>
        <div>
          <h1>{document.title}</h1>
          <p>{document.owner} 更新于 {document.updatedAt}</p>
        </div>
        <div className={styles.actions}>
          <Button icon={<Star size={17} />}>收藏</Button>
          <Button icon={<Share2 size={17} />}>分享</Button>
          <Button icon={<Edit3 size={17} />}>编辑</Button>
          <Button icon={<MoreHorizontal size={17} />} />
        </div>
      </header>
      <section className={styles.body}>
        <p>{document.excerpt ?? '这里是文档正文预览。后续接入编辑器后，正文会从 document_versions.body 的 jsonb 块结构渲染。'}</p>
        <h2>首版目标</h2>
        <ul>
          <li>建立文档详情的阅读框架。</li>
          <li>保留编辑器接入空间。</li>
          <li>为评论、版本和权限入口预留操作区。</li>
        </ul>
      </section>
      <footer className={styles.commentBox}>
        <MessageSquare size={18} />
        <span>评论能力将在接口和编辑器稳定后接入。</span>
      </footer>
    </article>
  );
}
