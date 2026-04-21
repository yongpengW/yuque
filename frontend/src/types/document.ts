export interface DocumentSummary {
  id: number
  repositoryId: number
  title: string
  status: 'draft' | 'published' | 'archived' | 'deleted'
  updatedAt: string
}
