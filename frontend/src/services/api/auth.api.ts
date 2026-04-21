import { httpClient } from '../http/client'
import type { CurrentUser } from '../../types/auth'

export const authApi = {
  getCurrentUser: () => httpClient.get<CurrentUser>('/api/auth/me'),
}
