import { Outlet } from 'react-router-dom';
import styles from './PublicLayout.module.css';

export function PublicLayout() {
  return (
    <main className={styles.layout}>
      <Outlet />
    </main>
  );
}
