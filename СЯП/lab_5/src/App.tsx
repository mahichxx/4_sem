import React, { useState } from 'react';
import Button from './components/ui/Button';
import Input, { Name } from './components/ui/Input';
import Badge from './components/ui/Badge';
import LayoutCard from './components/ui/LayoutCard';

const App: React.FC = () => {
  const [username, setUsername] = useState('');
  const [email, setEmail] = useState('');

  return (
    <div style={{ padding: 32, display: 'flex', flexDirection: 'column', gap: 24, maxWidth: 600 }}>
      <LayoutCard
        title="Кнопки"
        footer={<span>Варианты и размеры</span>}
      >
        <div style={{ display: 'flex', gap: 12, flexWrap: 'wrap' }}>
          <Button variant="primary" size="small">Primary</Button>
          <Button variant="secondary" size="medium">Secondary</Button>
          <Button variant="danger" size="large">Danger</Button>
          <Button variant="primary" size="medium" isLoading>Save</Button>
        </div>
      </LayoutCard>

    <Name name="jdd"/>
      <LayoutCard title="Поля ввода">
        <div style={{ display: 'flex', flexDirection: 'column', gap: 12 }}>
          <Input
            label="Имя пользователя"
            value={username}
            onChange={(e: React.ChangeEvent<HTMLInputElement>) => setUsername(e.target.value)}
            placeholder="Введите имя"
          />
          <Input
            label="Email"
            value={email}
            onChange={(e: React.ChangeEvent<HTMLInputElement>) => setEmail(e.target.value)}
            error="Введите корректный email"
            isFullWidth
          />
        </div>
      </LayoutCard>

      <LayoutCard title="Бейд">
        <div style={{ display: 'flex', gap: 10 }}>
          <Badge color="green">online</Badge>
          <Badge color="red">banned</Badge>
          <Badge color="orange">pending</Badge>
          <Badge color="blue">admin</Badge>
        </div>
      </LayoutCard>
    </div>
  );
};

export default App;
