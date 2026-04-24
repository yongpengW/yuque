import { Button, Input } from 'antd';
import { BarChart3, Crown, KeyRound, Settings, ShieldCheck, UserRound } from 'lucide-react';
import styles from './SettingsProfilePage.module.css';

const settingsGroups = [
  ['个人信息'],
  ['偏好设置', '通用设置', '文档设置', '导航栏设置', '提醒设置', '消息设置', 'AI 助手设置'],
  ['安全日志', '账户管理'],
  ['数据与统计', '稻谷'],
  ['会员信息', '账单'],
  ['Token', '系统级应用', '授权'],
];

export function SettingsProfilePage() {
  return (
    <main className={styles.layout}>
      <aside className={styles.sidebar}>
        <a className={styles.back}>‹ 语雀　返回</a>
        <div className={styles.user}>
          <div className={styles.avatar}>L</div>
          <strong>Leo</strong>
          <span>u39376658</span>
        </div>
        <nav className={styles.nav}>
          {settingsGroups.flat().map((item, index) => (
            <a key={item} className={index === 0 ? styles.active : ''}>
              {index === 0 ? <UserRound size={19} /> : index === 1 ? <Settings size={19} /> : index > 12 ? <KeyRound size={19} /> : index > 8 ? <Crown size={19} /> : index > 6 ? <BarChart3 size={19} /> : <ShieldCheck size={19} />}
              {item}
            </a>
          ))}
        </nav>
      </aside>
      <section className={styles.content}>
        <h1>个人信息</h1>
        <div className={styles.form}>
          <label>头像</label>
          <div className={styles.avatarRow}>
            <div className={styles.largeAvatar}>L</div>
            <Button>更新头像</Button>
            <span>可以拖动图片到左边头像区域完成上传</span>
          </div>
          <label>昵称 *</label>
          <Input defaultValue="Leo" size="large" />
          <label>简介 <span>0 / 56</span></label>
          <Input.TextArea placeholder="简单介绍一下你自己" rows={6} />
          <div className={styles.double}>
            <div>
              <label>地址</label>
              <Input placeholder="你所在的地址" size="large" />
            </div>
            <div>
              <label>领域</label>
              <Input placeholder="你所在的行业" size="large" />
            </div>
          </div>
          <Button className={styles.saveButton} type="primary">更新信息</Button>
        </div>
      </section>
    </main>
  );
}
