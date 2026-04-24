import { useQuery } from '@tanstack/react-query';
import { Button, Input } from 'antd';
import { Home, ListTree, MoreHorizontal, Plus, Search } from 'lucide-react';
import { NavLink, Outlet, useParams } from 'react-router-dom';
import { EntityIcon } from '../../components/EntityIcon/EntityIcon';
import { getRepository } from '../../services/knowledgeService';
import styles from './RepoWorkspaceLayout.module.css';

export function RepoWorkspaceLayout() {
  const { repoKey = '' } = useParams();
  const { data: repo } = useQuery({
    queryKey: ['repository', repoKey],
    queryFn: () => getRepository(repoKey),
  });

  return (
    <div className={styles.layout}>
      <aside className={styles.repoSidebar}>
        <div className={styles.breadcrumb}>语雀 / 个人知识库</div>
        <div className={styles.repoTitle}>
          <EntityIcon type="repository" size="md" />
          <strong>{repo?.name ?? '知识库'}</strong>
          <MoreHorizontal size={18} />
        </div>
        <div className={styles.searchRow}>
          <Input prefix={<Search size={16} />} placeholder="搜索" suffix="Ctrl + J" />
          <Button icon={<Plus size={18} />} />
        </div>
        <NavLink to={`/r/${repoKey}`} end className={({ isActive }) => `${styles.repoNav} ${isActive ? styles.active : ''}`}>
          <Home size={17} />
          首页
        </NavLink>
        <div className={styles.catalogTitle}>
          <span>
            <ListTree size={16} />
            目录
          </span>
          <ListTree size={16} />
        </div>
        <nav className={styles.catalog}>
          {repo?.catalogGroups.map((group) => (
            <div key={group.id} className={styles.catalogGroup}>
              <strong>{group.title}</strong>
              {group.documents.map((doc) => (
                <NavLink key={doc.key} to={`/r/${repoKey}/d/${doc.key}`} className={styles.catalogLink}>
                  {doc.title}
                </NavLink>
              ))}
            </div>
          ))}
        </nav>
      </aside>
      <main className={styles.repoContent}>
        <Outlet />
      </main>
    </div>
  );
}
