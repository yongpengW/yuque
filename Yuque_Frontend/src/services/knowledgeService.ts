import { currentUser, favorites, knowledgeBases, notes, recentDocuments, repositoryDetails } from './mockData';

const wait = async () => new Promise((resolve) => setTimeout(resolve, 120));

export async function getCurrentUser() {
  await wait();
  return currentUser;
}

export async function getKnowledgeBases() {
  await wait();
  return knowledgeBases;
}

export async function getRecentDocuments() {
  await wait();
  return recentDocuments;
}

export async function getRepository(repoKey: string) {
  await wait();
  return repositoryDetails.find((repo) => repo.key === repoKey) ?? null;
}

export async function getDocument(repoKey: string, docKey: string) {
  await wait();
  return recentDocuments.find((doc) => doc.repositoryKey === repoKey && doc.key === docKey) ?? null;
}

export async function getNotes() {
  await wait();
  return notes;
}

export async function getNote(noteId?: string) {
  await wait();
  return notes.find((note) => note.id === noteId) ?? notes[0] ?? null;
}

export async function getFavorites() {
  await wait();
  return favorites;
}
