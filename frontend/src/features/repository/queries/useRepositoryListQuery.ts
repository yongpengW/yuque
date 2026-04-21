import { useQuery } from '@tanstack/react-query'
import { queryKeys } from '../../../constants/queryKeys'
import { repositoryApi } from '../../../services/api/repository.api'

export function useRepositoryListQuery() {
  return useQuery({
    queryKey: queryKeys.repositories,
    queryFn: () => repositoryApi.list(),
  })
}
