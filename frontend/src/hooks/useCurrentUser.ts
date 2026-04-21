import { useAuthStore } from '../app/store/authStore'

export function useCurrentUser() {
  return useAuthStore((state) => state.currentUser)
}
