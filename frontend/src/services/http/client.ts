type HttpMethod = 'GET' | 'POST' | 'PUT' | 'PATCH' | 'DELETE'

interface RequestOptions {
  method?: HttpMethod
  body?: unknown
  headers?: Record<string, string>
}

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL ?? 'http://localhost:5103'

async function buildError(response: Response) {
  try {
    const payload = (await response.json()) as {
      message?: string
      errors?: Record<string, string[]>
    }

    const error = new Error(payload.message ?? `Request failed with status ${response.status}`)
    ;(error as Error & { details?: Record<string, string[]> }).details = payload.errors
    return error
  } catch {
    return new Error(`Request failed with status ${response.status}`)
  }
}

async function request<T>(path: string, options: RequestOptions = {}): Promise<T> {
  const response = await fetch(`${API_BASE_URL}${path}`, {
    method: options.method ?? 'GET',
    headers: {
      'Content-Type': 'application/json',
      ...options.headers,
    },
    body: options.body ? JSON.stringify(options.body) : undefined,
  })

  if (!response.ok) {
    throw await buildError(response)
  }

  return (await response.json()) as T
}

export const httpClient = {
  get: <T>(path: string) => request<T>(path),
  post: <T>(path: string, body?: unknown) => request<T>(path, { method: 'POST', body }),
}
