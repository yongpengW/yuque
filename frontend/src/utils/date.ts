export function formatDateLabel(value: string) {
  return new Date(value).toLocaleString('zh-CN')
}
