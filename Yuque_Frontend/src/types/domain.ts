export type User = {
  id: string;
  name: string;
  handle: string;
  avatarUrl?: string;
  membershipText: string;
};

export type KnowledgeBase = {
  id: string;
  key: string;
  name: string;
  owner: string;
  description: string;
  isPrivate: boolean;
  documentCount: number;
  wordCount: number;
  updatedAt: string;
  pinnedDocuments: DocumentItem[];
};

export type DocumentItem = {
  id: string;
  key: string;
  title: string;
  repositoryKey: string;
  repositoryName: string;
  owner: string;
  updatedAt: string;
  type: 'document' | 'table' | 'board';
  excerpt?: string;
};

export type CatalogGroup = {
  id: string;
  title: string;
  documents: DocumentItem[];
};

export type RepositoryDetail = KnowledgeBase & {
  catalogGroups: CatalogGroup[];
};

export type NoteItem = {
  id: string;
  title: string;
  updatedAt: string;
  tags: string[];
  body: string[];
};

export type FavoriteItem = {
  id: string;
  targetType: 'repository' | 'document';
  name: string;
  owner: string;
  favoritedAt: string;
  repositoryKey?: string;
  documentKey?: string;
};
