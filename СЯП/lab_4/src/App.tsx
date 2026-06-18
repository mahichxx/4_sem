import { useState } from "react";

type ButtonProps = {
  title: string;
  onClick: () => void;
  disabled?: boolean;
};

const buttonStyle = (disabled: boolean): React.CSSProperties => ({
  padding: "12px 28px",
  fontSize: "18px",
  borderRadius: "8px",
  border: "none",
  cursor: disabled ? "not-allowed" : "pointer",
  backgroundColor: disabled ? "#5c8689" : "#00bcd4",
  color: disabled ? "#272c2d" : "#000000",
  fontWeight: "bold",
  transition: "background-color 0.2s",
});

const Button = ({ title, onClick, disabled = false }: ButtonProps) => {
  return (
    <button style={buttonStyle(disabled)} onClick={onClick} disabled={disabled}>
      {title}
    </button>
  );
};

const MAX_COUNT = 5;

const Counter = () => {
  const [count, setCount] = useState<number>(0);

  const handleIncrease = (): void => {
    setCount((prev) => prev + 1);
  };

  const handleReset = (): void => {
    setCount(0);
  };

  return (
    <div
      style={{
        backgroundColor: "#141414",
        border: "2px solid #00bcd4",
        borderRadius: "16px",
        padding: "60px 80px",
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
        gap: "40px",
        minWidth: "320px",
      }}
    >
      <span
        style={{
          fontSize: "72px",
          fontWeight: "bold",
          color: count >= MAX_COUNT ? "#f44336" : "#00bcd4",
          transition: "color 0.3s",
        }}
      >
        {count}
      </span>

      <div style={{ display: "flex", gap: "20px" }}>
        <Button
          title="increase"
          onClick={handleIncrease}
          disabled={count >= MAX_COUNT}
        />
        <Button
          title="reset"
          onClick={handleReset}
          disabled={count === 0}
        />
      </div>
    </div>
  );
};

const App = () => {
  return (
    <div
      style={{
        minHeight: "100vh",
        backgroundColor: "#121212",
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
      }}
    >
      <Counter />
    </div>
  );
};

export default App;

//npm run dev