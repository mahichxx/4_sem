import React from 'react';
import cn from 'classnames';
import s from './Button.module.css';

interface ButtonProps extends React.ButtonHTMLAttributes<HTMLButtonElement> {
  variant: 'primary' | 'secondary' | 'danger';
  size: 'small' | 'medium' | 'large';
  isLoading?: boolean;
}

const Button: React.FC<ButtonProps> = ({ variant, size, isLoading = false, children, ...rest }) => {
  return (
    <button
      className={cn(s.button, s[variant], s[size], { [s.loading]: isLoading })}
      disabled={isLoading || rest.disabled}
      {...rest}
    >
      {isLoading ? 'Загрузка...' : children}
    </button>
  );
};

export default Button;
