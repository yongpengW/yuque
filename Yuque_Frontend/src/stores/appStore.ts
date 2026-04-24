import { create } from 'zustand';

type AppState = {
  currentWorkspaceKey: string;
  isSidebarCollapsed: boolean;
  setCurrentWorkspaceKey: (workspaceKey: string) => void;
  setSidebarCollapsed: (collapsed: boolean) => void;
};

export const useAppStore = create<AppState>((set) => ({
  currentWorkspaceKey: 'personal',
  isSidebarCollapsed: false,
  setCurrentWorkspaceKey: (workspaceKey) => set({ currentWorkspaceKey: workspaceKey }),
  setSidebarCollapsed: (collapsed) => set({ isSidebarCollapsed: collapsed }),
}));
