import React, { useState } from "react";
import { useAuth } from "../AuthContext";

type Props = {
  onSuccess?: () => void;
};

export const AuthForm: React.FC<Props> = ({ onSuccess }) => {
  const { login } = useAuth();
  const [name, setName] = useState("");
  const [email, setEmail] = useState("");

  const onSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    if (name.trim().length < 2) return alert("Имя слишком короткое");
    if (!email.includes("@")) return alert("Некорректный email");

    login({ name, email });
    onSuccess?.();
  };

  return (
    <div style={{ maxWidth: 400, margin: "0 auto" }}>
      <h2>Вход</h2>
      <form onSubmit={onSubmit}>
        <div>
          <label>Имя</label>
          <input value={name} onChange={(e) => setName(e.target.value)} />
        </div>

        <div>
          <label>Email</label>
          <input value={email} onChange={(e) => setEmail(e.target.value)} />
        </div>

        <button type="submit" style={{ marginTop: 8 }}>
          Войти
        </button>
      </form>
    </div>
  );
};
