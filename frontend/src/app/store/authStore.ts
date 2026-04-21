import { create } from 'zustand'
import type { CurrentUser } from '../../types/auth'

interface AuthState {
  accessToken: string | null
  currentUser: CurrentUser | null
  setSession: (payload: { accessToken: string; currentUser: CurrentUser }) => void
  clearSession: () => void
}

export const useAuthStore = create<AuthState>((set) => ({
  accessToken: null,
  currentUser: null,
  setSession: ({ accessToken, currentUser }) => set({ accessToken, currentUser }),
  clearSession: () => set({ accessToken: null, currentUser: null }),
}))
