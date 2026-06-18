import React from 'react';
import cn from 'classnames';
import s from './Input.module.css';

interface InputProps extends React.InputHTMLAttributes<HTMLInputElement> {
  label: string;
  error?: string;
  isFullWidth?: boolean;
}

const Input: React.FC<InputProps> = ({ label, error, isFullWidth = false, ...rest }) => {
  return (
    <div className={cn(s.wrapper, { [s.fullWidth]: isFullWidth })}>
      <label className={s.label}>{label}</label>
      <input className={cn(s.input, { [s.hasError]: error })} {...rest} />
      {error && <span className={s.error}>{error}</span>}
    </div>
  );
};

interface NameUser {
  name:string;
}


export const Name: React.FC<NameUser> =({ name}) => {
  return (
  <div>{name}</div>
);
}


export default Input;
