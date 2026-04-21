import { httpClient } from '../http/client'
import type { DocumentSummary } from '../../types/document'

interface CreateDocumentPayload {
  repositoryId: number
  title: string
}

export const documentApi = {
  list: () => httpClient.get<DocumentSummary[]>('/api/documents'),
  create: (payload: CreateDocumentPayload) =>
    httpClient.post<DocumentSummary>('/api/documents', payload),
}
