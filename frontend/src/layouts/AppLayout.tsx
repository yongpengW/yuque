import { Layout, Typography } from 'antd'
import { Outlet } from 'react-router-dom'

const { Header, Content } = Layout
const { Title, Text } = Typography

export function AppLayout() {
  return (
    <Layout style={{ minHeight: '100vh' }}>
      <Header
        style={{
          display: 'flex',
          alignItems: 'center',
          background: '#101828',
          color: '#fff',
        }}
      >
        <Title level={4} style={{ margin: 0, color: '#fff' }}>
          Yuque Clone
        </Title>
      </Header>
      <Content style={{ padding: 24 }}>
        <Text type="secondary">应用工作台骨架已就绪</Text>
        <div style={{ marginTop: 16 }}>
          <Outlet />
        </div>
      </Content>
    </Layout>
  )
}
