import { useQuery } from '@tanstack/react-query';
import { Button } from 'antd';
import { Feather, Grid2X2, List, MoreHorizontal, Search, Tag } from 'lucide-react';
import { NavLink, Outlet } from 'react-router-dom';
import { getNotes } from '../../services/knowledgeService';
import styles from './NotesLayout.module.css';

export function NotesLayout() {
  const { data: notes = [] } = useQuery({ queryKey: ['notes'], queryFn: getNotes });

  return (
    <div className={styles.layout}>
      <aside className={styles.panel}>
        <header className={styles.header}>
          <h1>小记</h1>
          <div className={styles.viewToggle}>
            <Button icon={<List size={16} />} />
            <Button icon={<Grid2X2 size={16} />} />
          </div>
          <Button className={styles.addButton} icon={<Feather size={18} />} />
        </header>
        <div className={styles.tools}>
          <span>
            <Tag size={16} />
            标签
          </span>
          <div>
            <Search size={18} />
            <MoreHorizontal size={18} />
          </div>
        </div>
        <nav className={styles.noteList}>
          {notes.map((note) => (
            <NavLink
              key={note.id}
              to={`/notes/${note.id}`}
              className={({ isActive }) => `${styles.noteItem} ${isActive ? styles.active : ''}`}
            >
              <span>{note.title}</span>
              <time>{note.updatedAt}</time>
            </NavLink>
          ))}
        </nav>
      </aside>
      <Outlet />
    </div>
  );
}
