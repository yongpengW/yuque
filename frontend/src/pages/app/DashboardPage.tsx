import { Button, Card, Col, Row, Space, Tag, Typography } from 'antd'
import { Link } from 'react-router-dom'
import { routes } from '../../constants/routes'

const { Title, Paragraph } = Typography

export function DashboardPage() {
  return (
    <div>
      <Title level={3}>工作台</Title>
      <Paragraph type="secondary">
        这里将承载最近访问、知识库列表、我的协作和通知聚合等业务模块。
      </Paragraph>
      <Space size={[8, 8]} wrap style={{ marginBottom: 16 }}>
        <Tag color="blue">features</Tag>
        <Tag color="purple">services</Tag>
        <Tag color="green">store</Tag>
        <Tag color="gold">editor</Tag>
      </Space>
      <Row gutter={[16, 16]}>
        <Col xs={24} md={12}>
          <Card
            title="Repositories"
            extra={
              <Button type="link">
                <Link to={routes.repositories}>查看列表</Link>
              </Button>
            }
          >
            知识库模块已经接入查询页面骨架。
          </Card>
        </Col>
        <Col xs={24} md={12}>
          <Card
            title="Documents"
            extra={
              <Button type="link">
                <Link to={routes.documents}>查看列表</Link>
              </Button>
            }
          >
            文档模块已经接入查询与创建页面骨架。
          </Card>
        </Col>
      </Row>
    </div>
  )
}
