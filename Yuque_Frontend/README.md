# Yuque Frontend

Independent frontend application for the Yuque-style knowledge base product.

## Stack

- Vite
- React
- TypeScript
- Ant Design
- React Router
- Zustand
- TanStack Query
- CSS Modules

## Commands

```powershell
npm install
npm run dev
npm run build
npm run preview
```

## Environment

Create a local env file when the backend API is ready:

```text
VITE_API_BASE_URL=http://127.0.0.1:5000/api
```

The current shell uses mock services under `src/services`. Replace those service functions with HTTP calls when the content API is ready.
