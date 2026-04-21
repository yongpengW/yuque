import { useQuery } from '@tanstack/react-query'
import { queryKeys } from '../../../constants/queryKeys'
import { documentApi } from '../../../services/api/document.api'

export function useDocumentListQuery() {
  return useQuery({
    queryKey: queryKeys.documents,
    queryFn: () => documentApi.list(),
  })
}
