import { useMutation, useQueryClient } from '@tanstack/react-query'
import { queryKeys } from '../../../constants/queryKeys'
import { repositoryApi } from '../../../services/api/repository.api'

interface CreateRepositoryPayload {
  name: string
  slug?: string
  visibility: 'private' | 'team' | 'public'
}

export function useCreateRepositoryMutation() {
  const queryClient = useQueryClient()

  return useMutation({
    mutationFn: (payload: CreateRepositoryPayload) => repositoryApi.create(payload),
    onSuccess: async () => {
      await queryClient.invalidateQueries({ queryKey: queryKeys.repositories })
    },
  })
}
