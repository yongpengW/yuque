import { useQuery } from '@tanstack/react-query';
import { Button } from 'antd';
import { Bold, CheckSquare, Link, List, Redo2, Undo2 } from 'lucide-react';
import { useParams } from 'react-router-dom';
import { getNote } from '../../services/knowledgeService';
import styles from './NotesPage.module.css';

export function NotesPage() {
  const { noteId } = useParams();
  const { data: note } = useQuery({ queryKey: ['note', noteId], queryFn: () => getNote(noteId) });

  if (!note) {
    return <main className={styles.page}>暂无小记</main>;
  }

  return (
    <main className={styles.page}>
      <div className={styles.toolbar}>
        {[Undo2, Redo2, Bold, List, CheckSquare, Link].map((Icon, index) => (
          <Button key={index} type="text" icon={<Icon size={19} />} />
        ))}
      </div>
      <article className={styles.editor}>
        <h1>{note.title}</h1>
        {note.body.map((line) => (
          <p key={line}>{line}</p>
        ))}
      </article>
    </main>
  );
}
