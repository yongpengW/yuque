import { useMutation, useQueryClient } from '@tanstack/react-query'
import { queryKeys } from '../../../constants/queryKeys'
import { documentApi } from '../../../services/api/document.api'

interface CreateDocumentPayload {
  repositoryId: number
  title: string
}

export function useCreateDocumentMutation() {
  const queryClient = useQueryClient()

  return useMutation({
    mutationFn: (payload: CreateDocumentPayload) => documentApi.create(payload),
    onSuccess: async () => {
      await queryClient.invalidateQueries({ queryKey: queryKeys.documents })
    },
  })
}
