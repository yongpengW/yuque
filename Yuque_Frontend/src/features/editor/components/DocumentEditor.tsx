import { useEffect, useRef, useState } from 'react';
import { Button, Dropdown, Tooltip, type MenuProps } from 'antd';
import Image from '@tiptap/extension-image';
import Link from '@tiptap/extension-link';
import Placeholder from '@tiptap/extension-placeholder';
import { Table, TableCell, TableHeader, TableRow } from '@tiptap/extension-table';
import TaskItem from '@tiptap/extension-task-item';
import TaskList from '@tiptap/extension-task-list';
import Underline from '@tiptap/extension-underline';
import { EditorContent, useEditor } from '@tiptap/react';
import StarterKit from '@tiptap/starter-kit';
import {
  Bold,
  CheckSquare,
  Code2,
  Heading1,
  Heading2,
  ImageIcon,
  Italic,
  Link2,
  List,
  ListOrdered,
  Pilcrow,
  Quote,
  Redo2,
  Save,
  Strikethrough,
  Table2,
  Underline as UnderlineIcon,
  Undo2,
} from 'lucide-react';
import type { EditorSaveStatus, MockEditorDocument } from '../types/editorTypes';
import styles from '../editor.module.css';

type DocumentEditorProps = {
  document: MockEditorDocument;
};

const saveStatusText: Record<EditorSaveStatus, string> = {
  saved: '已保存',
  saving: '保存中...',
  failed: '保存失败',
};

export function DocumentEditor({ document }: DocumentEditorProps) {
  const [title, setTitle] = useState(document.title);
  const [saveStatus, setSaveStatus] = useState<EditorSaveStatus>('saved');
  const saveTimerRef = useRef<number | null>(null);

  const markDirty = () => {
    setSaveStatus('saving');

    if (saveTimerRef.current) {
      window.clearTimeout(saveTimerRef.current);
    }

    saveTimerRef.current = window.setTimeout(() => {
      setSaveStatus('saved');
    }, 700);
  };

  const editor = useEditor({
    editable: document.canEdit,
    extensions: [
      StarterKit.configure({
        heading: { levels: [1, 2, 3] },
        link: false,
        underline: false,
      }),
      Underline,
      Link.configure({
        openOnClick: false,
        autolink: true,
      }),
      Placeholder.configure({
        placeholder: ({ node }) => {
          if (node.type.name === 'heading') {
            return '请输入标题';
          }

          return '输入 / 唤起更多';
        },
      }),
      TaskList,
      TaskItem.configure({
        nested: true,
      }),
      Table.configure({
        resizable: true,
      }),
      TableRow,
      TableHeader,
      TableCell,
      Image,
    ],
    content: document.body,
    editorProps: {
      attributes: {
        'aria-label': '文档正文编辑器',
      },
    },
    onUpdate: markDirty,
  });

  useEffect(() => {
    return () => {
      if (saveTimerRef.current) {
        window.clearTimeout(saveTimerRef.current);
      }
    };
  }, []);

  useEffect(() => {
    editor?.setEditable(document.canEdit);
  }, [document.canEdit, editor]);

  if (!editor) {
    return null;
  }

  const toggleLink = () => {
    const previousUrl = editor.getAttributes('link').href as string | undefined;
    const url = window.prompt('请输入链接地址', previousUrl ?? 'https://');

    if (url === null) {
      return;
    }

    if (url.trim() === '') {
      editor.chain().focus().extendMarkRange('link').unsetLink().run();
      return;
    }

    editor.chain().focus().extendMarkRange('link').setLink({ href: url }).run();
  };

  const insertImage = () => {
    const url = window.prompt('请输入图片 URL', 'https://');

    if (!url) {
      return;
    }

    editor.chain().focus().setImage({ src: url }).run();
  };

  const convertMenuItems: MenuProps['items'] = [
    {
      key: 'paragraph',
      icon: <Pilcrow size={16} />,
      label: '正文',
      onClick: () => editor.chain().focus().setParagraph().run(),
    },
    {
      key: 'heading1',
      icon: <Heading1 size={16} />,
      label: '一级标题',
      onClick: () => editor.chain().focus().toggleHeading({ level: 1 }).run(),
    },
    {
      key: 'heading2',
      icon: <Heading2 size={16} />,
      label: '二级标题',
      onClick: () => editor.chain().focus().toggleHeading({ level: 2 }).run(),
    },
    {
      key: 'quote',
      icon: <Quote size={16} />,
      label: '引用块',
      onClick: () => editor.chain().focus().toggleBlockquote().run(),
    },
    {
      key: 'codeBlock',
      icon: <Code2 size={16} />,
      label: '代码块',
      onClick: () => editor.chain().focus().toggleCodeBlock().run(),
    },
    {
      key: 'taskList',
      icon: <CheckSquare size={16} />,
      label: '待办列表',
      onClick: () => editor.chain().focus().toggleTaskList().run(),
    },
  ];

  const toolbarItems = [
    {
      key: 'undo',
      label: '撤销',
      icon: <Undo2 size={16} />,
      onClick: () => editor.chain().focus().undo().run(),
      disabled: !editor.can().undo(),
    },
    {
      key: 'redo',
      label: '重做',
      icon: <Redo2 size={16} />,
      onClick: () => editor.chain().focus().redo().run(),
      disabled: !editor.can().redo(),
    },
    {
      key: 'heading1',
      label: '一级标题',
      icon: <Heading1 size={16} />,
      active: editor.isActive('heading', { level: 1 }),
      onClick: () => editor.chain().focus().toggleHeading({ level: 1 }).run(),
    },
    {
      key: 'heading2',
      label: '二级标题',
      icon: <Heading2 size={16} />,
      active: editor.isActive('heading', { level: 2 }),
      onClick: () => editor.chain().focus().toggleHeading({ level: 2 }).run(),
    },
    {
      key: 'bold',
      label: '加粗',
      icon: <Bold size={16} />,
      active: editor.isActive('bold'),
      onClick: () => editor.chain().focus().toggleBold().run(),
    },
    {
      key: 'italic',
      label: '斜体',
      icon: <Italic size={16} />,
      active: editor.isActive('italic'),
      onClick: () => editor.chain().focus().toggleItalic().run(),
    },
    {
      key: 'underline',
      label: '下划线',
      icon: <UnderlineIcon size={16} />,
      active: editor.isActive('underline'),
      onClick: () => editor.chain().focus().toggleUnderline().run(),
    },
    {
      key: 'strike',
      label: '删除线',
      icon: <Strikethrough size={16} />,
      active: editor.isActive('strike'),
      onClick: () => editor.chain().focus().toggleStrike().run(),
    },
    {
      key: 'bulletList',
      label: '无序列表',
      icon: <List size={16} />,
      active: editor.isActive('bulletList'),
      onClick: () => editor.chain().focus().toggleBulletList().run(),
    },
    {
      key: 'orderedList',
      label: '有序列表',
      icon: <ListOrdered size={16} />,
      active: editor.isActive('orderedList'),
      onClick: () => editor.chain().focus().toggleOrderedList().run(),
    },
    {
      key: 'taskList',
      label: '待办列表',
      icon: <CheckSquare size={16} />,
      active: editor.isActive('taskList'),
      onClick: () => editor.chain().focus().toggleTaskList().run(),
    },
    {
      key: 'blockquote',
      label: '引用',
      icon: <Quote size={16} />,
      active: editor.isActive('blockquote'),
      onClick: () => editor.chain().focus().toggleBlockquote().run(),
    },
    {
      key: 'codeBlock',
      label: '代码块',
      icon: <Code2 size={16} />,
      active: editor.isActive('codeBlock'),
      onClick: () => editor.chain().focus().toggleCodeBlock().run(),
    },
    {
      key: 'link',
      label: '链接',
      icon: <Link2 size={16} />,
      active: editor.isActive('link'),
      onClick: toggleLink,
    },
    {
      key: 'table',
      label: '插入表格',
      icon: <Table2 size={16} />,
      onClick: () => editor.chain().focus().insertTable({ rows: 3, cols: 3, withHeaderRow: true }).run(),
    },
    {
      key: 'image',
      label: '插入图片',
      icon: <ImageIcon size={16} />,
      onClick: insertImage,
    },
  ];

  return (
    <article className={styles.editorShell}>
      <header className={styles.topbar}>
        <span>{title || '未命名文档'}</span>
        <span className={`${styles.saveState} ${styles[saveStatus]}`}>
          <Save size={14} />
          {saveStatusText[saveStatus]}
        </span>
      </header>

      <div className={styles.toolbar}>
        <Dropdown menu={{ items: convertMenuItems }} trigger={['click']}>
          <Button className={styles.textTool}>转换为</Button>
        </Dropdown>
        <Button
          className={editor.isActive('paragraph') ? `${styles.textTool} ${styles.activeTool}` : styles.textTool}
          icon={<Pilcrow size={15} />}
          onClick={() => editor.chain().focus().setParagraph().run()}
        >
          正文
        </Button>
        <Button
          className={editor.isActive('heading', { level: 2 }) ? `${styles.textTool} ${styles.activeTool}` : styles.textTool}
          icon={<Heading2 size={15} />}
          onClick={() => editor.chain().focus().toggleHeading({ level: 2 }).run()}
        >
          标题
        </Button>
        <Button
          className={editor.isActive('codeBlock') ? `${styles.textTool} ${styles.activeTool}` : styles.textTool}
          icon={<Code2 size={15} />}
          onClick={() => editor.chain().focus().toggleCodeBlock().run()}
        >
          代码块
        </Button>
        <span className={styles.toolbarDivider} />
        {toolbarItems.map((item) => (
          <Tooltip key={item.key} title={item.label}>
            <Button
              className={item.active ? styles.activeTool : undefined}
              type="text"
              icon={item.icon}
              disabled={item.disabled}
              onClick={item.onClick}
            />
          </Tooltip>
        ))}
      </div>

      <section className={styles.canvas}>
        <textarea
          className={styles.titleInput}
          value={title}
          disabled={!document.canEdit}
          placeholder="请输入标题"
          rows={1}
          onChange={(event) => {
            setTitle(event.target.value);
            markDirty();
          }}
        />
        <div className={styles.hint}>正文区域已接入 Tiptap，当前使用前端 mock 数据，暂不连接后端。</div>
        <EditorContent className={styles.editorContent} editor={editor} onClick={() => editor.chain().focus().run()} />
      </section>
    </article>
  );
}
