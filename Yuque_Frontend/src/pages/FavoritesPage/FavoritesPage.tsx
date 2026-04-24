import { useQuery } from '@tanstack/react-query';
import { Button } from 'antd';
import { Filter, Search, Star } from 'lucide-react';
import { Link } from 'react-router-dom';
import { getFavorites } from '../../services/knowledgeService';
import styles from './FavoritesPage.module.css';

export function FavoritesPage() {
  const { data: favorites = [] } = useQuery({ queryKey: ['favorites'], queryFn: getFavorites });

  return (
    <main className={styles.page}>
      <header className={styles.header}>
        <h1>收藏</h1>
        <div>
          <Button type="text" icon={<Search size={21} />} />
          <Button type="text" icon={<Filter size={21} />} />
        </div>
      </header>
      <section className={styles.table}>
        <div className={styles.tableHead}>
          <span>名称 ↑↓</span>
          <span>归属</span>
          <span>收藏时间 ↑↓</span>
          <span />
        </div>
        {favorites.map((item) => (
          <Link key={item.id} to={item.repositoryKey ? `/r/${item.repositoryKey}` : '#'} className={styles.row}>
            <span className={styles.name}>
              <span className={styles.bookIcon} />
              {item.name}
            </span>
            <span>{item.owner}</span>
            <time>{item.favoritedAt}</time>
            <Star size={22} fill="#ffb000" color="#ffb000" />
          </Link>
        ))}
      </section>
    </main>
  );
}
