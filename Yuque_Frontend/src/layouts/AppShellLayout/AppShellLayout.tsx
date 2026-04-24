import { Button, Input, Tooltip } from 'antd';
import {
  Bell,
  CircleEllipsis,
  Clock3,
  Feather,
  FolderOpen,
  Plus,
  Search,
  Sparkles,
  Star,
  UsersRound,
} from 'lucide-react';
import { NavLink, Outlet } from 'react-router-dom';
import { useQuery } from '@tanstack/react-query';
import { EntityIcon } from '../../components/EntityIcon/EntityIcon';
import { getCurrentUser, getKnowledgeBases } from '../../services/knowledgeService';
import styles from './AppShellLayout.module.css';

const mainNav = [
  { to: '/home', label: '开始', icon: Clock3 },
  { to: '/ai-writing', label: 'AI 写作', icon: Sparkles },
  { to: '/notes', label: '小记', icon: Feather },
  { to: '/favorites', label: '收藏', icon: Star },
  { to: '/traffic', label: '逛逛', icon: UsersRound },
];

export function AppShellLayout() {
  const { data: user } = useQuery({ queryKey: ['currentUser'], queryFn: getCurrentUser });
  const { data: knowledgeBases = [] } = useQuery({ queryKey: ['knowledgeBases'], queryFn: getKnowledgeBases });

  return (
    <div className={styles.shell}>
      <aside className={styles.sidebar}>
        <div className={styles.brandRow}>
          <NavLink to="/home" className={styles.brand}>
            <span className={styles.logo}>语</span>
            <strong>语雀</strong>
          </NavLink>
          <Bell size={18} strokeWidth={1.8} />
          <div className={styles.avatar}>{user?.name.slice(0, 1) ?? 'L'}</div>
        </div>

        <div className={styles.searchRow}>
          <Input
            className={styles.searchInput}
            prefix={<Search size={16} />}
            placeholder="搜索"
            suffix={<span className={styles.shortcut}>Ctrl J</span>}
          />
          <Tooltip title="新建">
            <Button className={styles.createButton} icon={<Plus size={18} />} />
          </Tooltip>
        </div>

        <nav className={styles.navList}>
          {mainNav.map((item) => (
            <NavLink
              key={item.to}
              to={item.to}
              className={({ isActive }) => `${styles.navItem} ${isActive ? styles.active : ''}`}
            >
              <item.icon size={18} strokeWidth={1.9} />
              <span>{item.label}</span>
            </NavLink>
          ))}
        </nav>

        <div className={styles.sectionTitle}>
          <span>知识库</span>
          <FolderOpen size={15} />
        </div>

        <div className={styles.repoList}>
          {knowledgeBases.map((repo) => (
            <NavLink
              key={repo.key}
              to={`/r/${repo.key}`}
              className={({ isActive }) => `${styles.repoItem} ${isActive ? styles.repoActive : ''}`}
            >
              <EntityIcon type="repository" size="sm" />
              <span className={styles.repoName}>{repo.name}</span>
              {repo.isPrivate ? <span className={styles.lock}>锁</span> : null}
            </NavLink>
          ))}
        </div>

        <div className={styles.sidebarFooter}>
          <NavLink to="/settings/profile" className={styles.moreLink}>
            <CircleEllipsis size={18} />
            <span>更多</span>
          </NavLink>
        </div>
      </aside>

      <section className={styles.content}>
        <Outlet />
      </section>
    </div>
  );
}
