import { Button, Checkbox, Divider, Input } from 'antd';
import { KeyRound, Mail, MessageCircle } from 'lucide-react';
import styles from './LoginPage.module.css';

export function LoginPage() {
  return (
    <div className={styles.page}>
      <section className={styles.card}>
        <div className={styles.logoBlock}>
          <span className={styles.logo}>语</span>
          <strong>语雀</strong>
        </div>

        <Input addonBefore="+86" defaultValue="13256873823" size="large" />
        <div className={styles.slider}>请 按 住 滑 块，拖动到最右边</div>
        <div className={styles.codeRow}>
          <Input placeholder="6 位短信验证码" size="large" />
          <Button size="large">获取验证码</Button>
        </div>
        <Button className={styles.loginButton} type="primary" size="large">
          登录 / 注册
        </Button>
        <Checkbox>
          我已阅读并同意语雀 <a>服务协议</a> 和 <a>隐私权政策</a>
        </Checkbox>

        <Divider>其他登录方式</Divider>
        <div className={styles.altRow}>
          <Button icon={<KeyRound size={16} />}>密码登录</Button>
          <Button shape="circle" icon={<MessageCircle size={18} />} />
          <Button shape="circle" icon={<Mail size={18} />} />
        </div>
      </section>
      <footer className={styles.footer}>中文 / English　|　遇到问题</footer>
    </div>
  );
}
