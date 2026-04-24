import type { DocumentItem, FavoriteItem, KnowledgeBase, NoteItem, RepositoryDetail, User } from '../types/domain';

export const currentUser: User = {
  id: 'u39376658',
  name: 'Leo',
  handle: 'u39376658',
  membershipText: '专业会员，2026-05-22到期',
};

export const recentDocuments: DocumentItem[] = [
  {
    id: 'doc-1',
    key: 'bgk-micro40',
    title: 'BGK-Micro40客户端开发需求文档',
    repositoryKey: 'leo-knowledge',
    repositoryName: 'Leo的知识库',
    owner: 'Leo',
    updatedAt: '02-09 16:19',
    type: 'document',
    excerpt: '用于记录客户端功能、页面流程和交付范围。',
  },
  {
    id: 'doc-2',
    key: 'luxury-auction',
    title: '奢侈品在线拍卖交易平台功能评估文档',
    repositoryKey: 'leo-knowledge',
    repositoryName: 'Leo的知识库',
    owner: 'Leo',
    updatedAt: '2025-07-17',
    type: 'document',
    excerpt: '评估拍卖交易平台的业务闭环、风控和结算流程。',
  },
  {
    id: 'doc-3',
    key: 'cw-china-erp-pos',
    title: 'CW China ERP POS',
    repositoryKey: 'leo-knowledge',
    repositoryName: 'Leo的知识库',
    owner: 'Leo',
    updatedAt: '2025-04-03',
    type: 'document',
  },
  {
    id: 'doc-4',
    key: 'game-notes',
    title: '游戏备忘',
    repositoryKey: 'leo-knowledge',
    repositoryName: 'Leo的知识库',
    owner: 'Leo',
    updatedAt: '2025-04-02',
    type: 'document',
  },
  {
    id: 'doc-5',
    key: 'memo',
    title: '备忘录',
    repositoryKey: 'leo-knowledge',
    repositoryName: 'Leo的知识库',
    owner: 'Leo',
    updatedAt: '2025-04-02',
    type: 'document',
  },
  {
    id: 'doc-6',
    key: 'english-class',
    title: '英文课堂',
    repositoryKey: 'leo-knowledge',
    repositoryName: 'Leo的知识库',
    owner: 'Leo',
    updatedAt: '2025-04-02',
    type: 'document',
  },
];

export const knowledgeBases: KnowledgeBase[] = [
  {
    id: 'repo-1',
    key: 'leo-knowledge',
    name: 'Leo的知识库',
    owner: 'Leo',
    description: '知识库就像书一样，让多篇文档结构化，方便知识的创作与沉淀',
    isPrivate: true,
    documentCount: 27,
    wordCount: 13764,
    updatedAt: '2025-07-17 21:18',
    pinnedDocuments: recentDocuments.slice(0, 3),
  },
];

export const repositoryDetails: RepositoryDetail[] = [
  {
    ...knowledgeBases[0],
    catalogGroups: [
      {
        id: 'work',
        title: '工作',
        documents: recentDocuments.slice(0, 5),
      },
      {
        id: 'life',
        title: '生活',
        documents: [
          recentDocuments[5],
          {
            id: 'doc-7',
            key: 'ladder',
            title: '梯子',
            repositoryKey: 'leo-knowledge',
            repositoryName: 'Leo的知识库',
            owner: 'Leo',
            updatedAt: '2025-04-02',
            type: 'document',
          },
          {
            id: 'doc-8',
            key: 'writing-template',
            title: '常用写作板',
            repositoryKey: 'leo-knowledge',
            repositoryName: 'Leo的知识库',
            owner: 'Leo',
            updatedAt: '2025-04-02',
            type: 'document',
          },
        ],
      },
      {
        id: 'games',
        title: '游戏',
        documents: [recentDocuments[3]],
      },
    ],
  },
];

export const notes: NoteItem[] = [
  {
    id: 'note-1',
    title: 'PC购买计划',
    updatedAt: '12-08',
    tags: ['标签'],
    body: [
      '搭配1：',
      '2025-12-8',
      '9950x+华硕小吹雪板u套装 京东自营 4594元',
      '显卡铭瑄瑷珈5070Ti OC 16G PDD 6299元',
      '内存25年涨价太高 等降价 1000块 64G',
      '共计预算：4594+6299+1000+429+339=12661元',
    ],
  },
  {
    id: 'note-2',
    title: 'Course Reporting Service',
    updatedAt: '08-02',
    tags: ['工作'],
    body: ['报表服务接口整理', '同步课程、订单、结算相关数据。'],
  },
  {
    id: 'note-3',
    title: '国内供应链收货模板',
    updatedAt: '07-19',
    tags: ['模板'],
    body: ['供应商、物流单号、预计到货时间、验收状态。'],
  },
];

export const favorites: FavoriteItem[] = [
  {
    id: 'fav-1',
    targetType: 'repository',
    name: 'Leo的知识库',
    owner: 'Leo',
    favoritedAt: '2025-04-02 16:33',
    repositoryKey: 'leo-knowledge',
  },
];
