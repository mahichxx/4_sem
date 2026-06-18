import RegistrationForm from './components/RegistrationForm'
import { CatalogPage } from './components/CatalogPage'
import { AuthProvider, useAuth } from './contexts/AuthContext'
import { ProductProvider } from './contexts/ProductContext'
import './components/RegistrationForm.css'

function AppContent() {
  const { isAuthenticated } = useAuth();
  return isAuthenticated ? <CatalogPage /> : <RegistrationForm />;
}

export default function App() {
  return (
    <AuthProvider>
      <ProductProvider>
        <AppContent />
      </ProductProvider>
    </AuthProvider>
  )
}