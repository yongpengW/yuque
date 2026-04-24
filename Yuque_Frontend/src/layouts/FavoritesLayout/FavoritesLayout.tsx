import { Plus } from 'lucide-react';
import { Outlet } from 'react-router-dom';
import styles from './FavoritesLayout.module.css';

export function FavoritesLayout() {
  return (
    <div className={styles.layout}>
      <aside className={styles.panel}>
        <div className={styles.groupCard}>
          <div>
            <strong>全部收藏</strong>
            <span>1 条内容</span>
          </div>
          <Plus size={19} />
        </div>
      </aside>
      <Outlet />
    </div>
  );
}
