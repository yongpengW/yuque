import { Alert, Button, Card, Form, Input, List, Modal, Select, Space, Tag, Typography, message } from 'antd'
import { useState } from 'react'
import { useCreateRepositoryMutation } from '../../../features/repository/mutations/useCreateRepositoryMutation'
import { useRepositoryListQuery } from '../../../features/repository/queries/useRepositoryListQuery'
import { formatDateLabel } from '../../../utils/date'

const { Title, Paragraph, Text } = Typography

interface CreateRepositoryFormValues {
  name: string
  slug?: string
  visibility: 'private' | 'team' | 'public'
}

export function RepositoryListPage() {
  const [form] = Form.useForm<CreateRepositoryFormValues>()
  const [messageApi, contextHolder] = message.useMessage()
  const [isCreateModalOpen, setIsCreateModalOpen] = useState(false)
  const repositoryListQuery = useRepositoryListQuery()
  const createRepositoryMutation = useCreateRepositoryMutation()

  async function handleCreateRepository(values: CreateRepositoryFormValues) {
    try {
      await createRepositoryMutation.mutateAsync(values)
      messageApi.success('知识库创建成功')
      setIsCreateModalOpen(false)
      form.resetFields()
    } catch (error) {
      const messageText = error instanceof Error ? error.message : '知识库创建失败'
      messageApi.error(messageText)
    }
  }

  return (
    <Space direction="vertical" size="large" style={{ display: 'flex' }}>
      {contextHolder}
      <div>
        <Title level={3} style={{ marginBottom: 8 }}>
          知识库列表
        </Title>
        <Paragraph type="secondary" style={{ margin: 0 }}>
          当前页面已经接入 TanStack Query 和后端知识库列表接口骨架，后续可以继续补创建知识库、权限配置和目录树入口。
        </Paragraph>
      </div>

      <Card
        title="Repositories"
        extra={
          <Button type="primary" onClick={() => setIsCreateModalOpen(true)}>
            新建知识库
          </Button>
        }
      >
        {repositoryListQuery.isError ? (
          <Alert
            type="error"
            message="知识库列表加载失败"
            description="当前后端接口未运行或数据库未初始化时，这里会出现请求错误。"
            showIcon
          />
        ) : null}

        <Modal
          title="新建知识库"
          open={isCreateModalOpen}
          onCancel={() => {
            setIsCreateModalOpen(false)
            form.resetFields()
          }}
          onOk={() => form.submit()}
          confirmLoading={createRepositoryMutation.isPending}
          destroyOnHidden
        >
          <Form
            form={form}
            layout="vertical"
            initialValues={{ visibility: 'private' }}
            onFinish={handleCreateRepository}
          >
            <Form.Item
              label="知识库名称"
              name="name"
              rules={[{ required: true, message: '请输入知识库名称' }]}
            >
              <Input maxLength={150} placeholder="例如：产品文档中心" />
            </Form.Item>
            <Form.Item label="Slug" name="slug">
              <Input maxLength={150} placeholder="例如：product-docs" />
            </Form.Item>
            <Form.Item label="可见性" name="visibility" rules={[{ required: true }]}>
              <Select
                options={[
                  { label: '私密', value: 'private' },
                  { label: '团队可见', value: 'team' },
                  { label: '公开', value: 'public' },
                ]}
              />
            </Form.Item>
          </Form>
        </Modal>

        <List
          loading={repositoryListQuery.isLoading || createRepositoryMutation.isPending}
          dataSource={repositoryListQuery.data ?? []}
          locale={{ emptyText: '暂无知识库数据' }}
          renderItem={(item) => (
            <List.Item>
              <List.Item.Meta
                title={
                  <Space>
                    <span>{item.name}</span>
                    <Tag>{item.visibility}</Tag>
                  </Space>
                }
                description={
                  <Space direction="vertical" size={0}>
                    <Text type="secondary">slug: {item.slug}</Text>
                    <Text type="secondary">更新时间: {formatDateLabel(item.updatedAt)}</Text>
                  </Space>
                }
              />
            </List.Item>
          )}
        />
      </Card>
    </Space>
  )
}
