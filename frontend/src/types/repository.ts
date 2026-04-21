export interface RepositorySummary {
  id: number
  name: string
  slug: string
  visibility: 'private' | 'team' | 'public'
  updatedAt: string
}
