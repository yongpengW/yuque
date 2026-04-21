import { Alert, Button, Card, Form, Input, List, Modal, Select, Space, Tag, Typography, message } from 'antd'
import { useState } from 'react'
import { useCreateDocumentMutation } from '../../../features/document/mutations/useCreateDocumentMutation'
import { useDocumentListQuery } from '../../../features/document/queries/useDocumentListQuery'
import { useRepositoryListQuery } from '../../../features/repository/queries/useRepositoryListQuery'
import { formatDateLabel } from '../../../utils/date'

const { Title, Paragraph, Text } = Typography

interface CreateDocumentFormValues {
  repositoryId: number
  title: string
}

export function DocumentListPage() {
  const [form] = Form.useForm<CreateDocumentFormValues>()
  const [messageApi, contextHolder] = message.useMessage()
  const [isCreateModalOpen, setIsCreateModalOpen] = useState(false)
  const documentListQuery = useDocumentListQuery()
  const repositoryListQuery = useRepositoryListQuery()
  const createDocumentMutation = useCreateDocumentMutation()

  async function handleCreateDocument(values: CreateDocumentFormValues) {
    try {
      await createDocumentMutation.mutateAsync(values)
      messageApi.success('文档创建成功')
      setIsCreateModalOpen(false)
      form.resetFields()
    } catch (error) {
      const messageText = error instanceof Error ? error.message : '文档创建失败'
      messageApi.error(messageText)
    }
  }

  return (
    <Space direction="vertical" size="large" style={{ display: 'flex' }}>
      {contextHolder}
      <div>
        <Title level={3} style={{ marginBottom: 8 }}>
          文档列表
        </Title>
        <Paragraph type="secondary" style={{ margin: 0 }}>
          当前页面已经接入 Documents 的 GET/POST 接口，并可通过知识库选择器创建新文档。
        </Paragraph>
      </div>

      <Card
        title="Documents"
        extra={
          <Button type="primary" onClick={() => setIsCreateModalOpen(true)}>
            新建文档
          </Button>
        }
      >
        {documentListQuery.isError ? (
          <Alert
            type="error"
            message="文档列表加载失败"
            description="当前后端接口未运行、数据库未初始化或网络不可达时，这里会出现请求错误。"
            showIcon
          />
        ) : null}

        <Modal
          title="新建文档"
          open={isCreateModalOpen}
          onCancel={() => {
            setIsCreateModalOpen(false)
            form.resetFields()
          }}
          onOk={() => form.submit()}
          confirmLoading={createDocumentMutation.isPending}
          destroyOnHidden
        >
          <Form form={form} layout="vertical" onFinish={handleCreateDocument}>
            <Form.Item
              label="所属知识库"
              name="repositoryId"
              rules={[{ required: true, message: '请选择所属知识库' }]}
            >
              <Select
                loading={repositoryListQuery.isLoading}
                options={(repositoryListQuery.data ?? []).map((item) => ({
                  label: `${item.name} (${item.slug})`,
                  value: item.id,
                }))}
                placeholder="请选择一个知识库"
              />
            </Form.Item>
            <Form.Item
              label="文档标题"
              name="title"
              rules={[{ required: true, message: '请输入文档标题' }]}
            >
              <Input maxLength={255} placeholder="例如：需求评审纪要" />
            </Form.Item>
          </Form>
        </Modal>

        <List
          loading={documentListQuery.isLoading || createDocumentMutation.isPending}
          dataSource={documentListQuery.data ?? []}
          locale={{ emptyText: '暂无文档数据' }}
          renderItem={(item) => (
            <List.Item>
              <List.Item.Meta
                title={
                  <Space>
                    <span>{item.title}</span>
                    <Tag>{item.status}</Tag>
                    <Tag color="blue">repo:{item.repositoryId}</Tag>
                  </Space>
                }
                description={<Text type="secondary">更新时间: {formatDateLabel(item.updatedAt)}</Text>}
              />
            </List.Item>
          )}
        />
      </Card>
    </Space>
  )
}
