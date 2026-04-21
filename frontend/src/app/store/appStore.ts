import { create } from 'zustand'

interface AppState {
  sidebarCollapsed: boolean
  currentRepositoryId: number | null
  setSidebarCollapsed: (collapsed: boolean) => void
  setCurrentRepositoryId: (repositoryId: number | null) => void
}

export const useAppStore = create<AppState>((set) => ({
  sidebarCollapsed: false,
  currentRepositoryId: null,
  setSidebarCollapsed: (sidebarCollapsed) => set({ sidebarCollapsed }),
  setCurrentRepositoryId: (currentRepositoryId) => set({ currentRepositoryId }),
}))
