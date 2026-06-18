import React from 'react';
import s from './LayoutCard.module.css';

interface LayoutCardProps {
  title: React.ReactNode;
  children: React.ReactNode;
  footer?: React.ReactNode;

}

const LayoutCard: React.FC<LayoutCardProps> = ({ title, children, footer }) => {
  return (
    <div className={s.card}>
      <div className={s.header}>{title}</div>
      <div></div>
      <div className={s.body}>{children}</div>
      {footer && <div className={s.footer}>{footer}</div>}
    </div>
  );
};

export default LayoutCard;
