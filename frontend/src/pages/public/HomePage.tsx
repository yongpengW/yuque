import { Button, Card, Space, Typography } from 'antd'
import { Link } from 'react-router-dom'
import { routes } from '../../constants/routes'

const { Title, Paragraph } = Typography

export function HomePage() {
  return (
    <div
      style={{
        minHeight: '100vh',
        display: 'grid',
        placeItems: 'center',
        padding: 24,
        background: 'linear-gradient(180deg, #f8fafc 0%, #e8eef7 100%)',
      }}
    >
      <Card style={{ maxWidth: 720, width: '100%', borderRadius: 20 }}>
        <Space direction="vertical" size="large">
          <div>
            <Title style={{ marginBottom: 8 }}>在线文档产品前端骨架</Title>
            <Paragraph style={{ margin: 0 }}>
              当前已接入 React、React Router、TanStack Query、Zustand 与 Ant Design，并补齐了 services、store、types、constants、editor 等基础骨架。
            </Paragraph>
          </div>
          <Space>
            <Button type="primary">
              <Link to={routes.dashboard}>进入工作台</Link>
            </Button>
          </Space>
        </Space>
      </Card>
    </div>
  )
}
