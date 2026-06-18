import { createContext, useContext, useState, useEffect, type ReactNode } from "react";

interface User {
    username: string;
}

interface AuthContextType {
    user: User | null;
    isAuthenticated: boolean;
    login: (user: User) => void;
    logout: () => void;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export function AuthProvider({ children }: { children: ReactNode }) {
    const [user, setUser] = useState<User | null>(null);
    const [isLoaded, setIsLoaded] = useState(false);

    useEffect(() => {
        const storedUser = localStorage.getItem("authUser");
        if (storedUser) {
            try {
                setUser(JSON.parse(storedUser));
            } catch (e) {
                console.error("Не удалось получить данные пользователя из localStorage.", e);
            }
        }
        setIsLoaded(true);
    }, []);

    const login = (newUser: User) => {
        setUser(newUser);
        localStorage.setItem("authUser", JSON.stringify(newUser));
    };

    const logout = () => {
        setUser(null);
        localStorage.removeItem("authUser");
    };

    if (!isLoaded) return null;

    return (
        <AuthContext.Provider value={{ user, isAuthenticated: !!user, login, logout }}>
            {children}
        </AuthContext.Provider>
    );
}

export function useAuth() {
    const context = useContext(AuthContext);
    if (!context) {
        throw new Error("Метод useAuth необходимо использовать внутри AuthProvider.");
    }
    return context;
}
