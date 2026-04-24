import styles from './EntityIcon.module.css';

type EntityIconProps = {
  type: 'repository' | 'document';
  size?: 'sm' | 'md' | 'lg';
};

const iconSize = {
  sm: 19,
  md: 18,
  lg: 26,
};

export function EntityIcon({ type, size = 'md' }: EntityIconProps) {
  if (type === 'document') {
    return (
      <span className={`${styles.icon} ${styles.document} ${styles[size]}`} aria-hidden="true">
        <svg width={iconSize[size]} height={iconSize[size] + 4} viewBox="0 0 18 22" fill="none">
          <rect x="2" y="1.5" width="14" height="19" rx="2" stroke="currentColor" strokeWidth="1.6" />
          <path d="M6 8h7M6 12h7M6 16h4.5" stroke="currentColor" strokeWidth="1.5" strokeLinecap="round" />
        </svg>
      </span>
    );
  }

  return (
    <span className={`${styles.icon} ${styles[type]} ${styles[size]}`} aria-hidden="true">
      <svg width={iconSize[size] + 4} height={iconSize[size] + 6} viewBox="0 0 22 26" fill="none">
        <path
          d="M4 2.5h12.5A2.5 2.5 0 0 1 19 5v16a2.5 2.5 0 0 1-2.5 2.5H4.8A1.8 1.8 0 0 1 3 21.7V3.5a1 1 0 0 1 1-1Z"
          fill="#9dccff"
          stroke="currentColor"
          strokeWidth="1.7"
        />
        <path d="M8 3v20.5" stroke="currentColor" strokeWidth="1.5" />
        <path d="M12 7.5h3.5" stroke="currentColor" strokeWidth="1.5" strokeLinecap="round" />
      </svg>
    </span>
  );
}
