import { httpClient } from '../http/client'
import type { RepositorySummary } from '../../types/repository'

interface CreateRepositoryPayload {
  name: string
  slug?: string
  visibility: 'private' | 'team' | 'public'
}

export const repositoryApi = {
  list: () => httpClient.get<RepositorySummary[]>('/api/repositories'),
  create: (payload: CreateRepositoryPayload) =>
    httpClient.post<RepositorySummary>('/api/repositories', payload),
}
