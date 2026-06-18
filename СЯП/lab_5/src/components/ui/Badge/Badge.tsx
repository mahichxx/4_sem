import React from 'react';
import s from './Badge.module.css';

interface BadgeProps {
  color: 'green' | 'red' | 'orange' | 'blue';
  children: React.ReactNode; 
  //text: string;

}

const Badge: React.FC<BadgeProps> = ({ color, children }) => {
  return <span className={`${s.badge} ${s[color]}`}>{children}</span>;
};

export default Badge;
