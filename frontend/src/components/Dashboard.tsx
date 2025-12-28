import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../AuthContext';
import { bootcampAPI } from '../api';
import './Dashboard.css';

interface Bootcamp {
    id: number;
    name: string;
    instructorId: number;
    startDate: string;
    endDate: string;
    bootcampState: string;
}

const Dashboard: React.FC = () => {
    const [bootcamps, setBootcamps] = useState<Bootcamp[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState('');
    const { user, logout } = useAuth();
    const navigate = useNavigate();

    useEffect(() => {
        fetchBootcamps();
    }, []);

    const fetchBootcamps = async () => {
        try {
            const response = await bootcampAPI.getAll();
            setBootcamps(response.data);
        } catch (err: any) {
            setError('Failed to load bootcamps');
        } finally {
            setLoading(false);
        }
    };

    const handleLogout = () => {
        logout();
        navigate('/login');
    };

    const getStatusBadge = (status: string) => {
        const badges: { [key: string]: string } = {
            Preparing: 'badge-secondary',
            Open: 'badge-success',
            Started: 'badge-primary',
            Completed: 'badge-warning',
        };
        return badges[status] || 'badge-secondary';
    };

    return (
        <div className="dashboard">
            <nav className="dashboard-nav glass">
                <div className="nav-brand">
                    <h2>🚀 Bootcamp</h2>
                </div>
                <div className="nav-menu">
                    <span className="user-info">
                        {user?.role === 'applicant' && '👨‍🎓'}
                        {user?.role === 'instructor' && '👨‍🏫'}
                        {user?.role === 'employee' && '👔'}
                        {' '}{user?.email}
                    </span>
                    <button className="btn btn-secondary" onClick={handleLogout}>
                        Logout
                    </button>
                </div>
            </nav>

            <div className="dashboard-content container">
                <div className="dashboard-header">
                    <h1 className="fade-in">Available Bootcamps</h1>
                    <p className="fade-in">Explore and apply to bootcamp programs</p>
                </div>

                {error && <div className="alert alert-error">{error}</div>}

                {loading ? (
                    <div className="loading-state">
                        <div className="spinner"></div>
                        <p>Loading bootcamps...</p>
                    </div>
                ) : bootcamps.length === 0 ? (
                    <div className="empty-state card">
                        <h3>No bootcamps available</h3>
                        <p>Check back later for new programs</p>
                    </div>
                ) : (
                    <div className="bootcamp-grid">
                        {bootcamps.map((bootcamp) => (
                            <div key={bootcamp.id} className="bootcamp-card card">
                                <div className="bootcamp-header">
                                    <h3>{bootcamp.name}</h3>
                                    <span className={`badge ${getStatusBadge(bootcamp.bootcampState)}`}>
                                        {bootcamp.bootcampState}
                                    </span>
                                </div>
                                <div className="bootcamp-info">
                                    <div className="info-item">
                                        <span className="info-label">📅 Start Date:</span>
                                        <span className="info-value">
                                            {new Date(bootcamp.startDate).toLocaleDateString()}
                                        </span>
                                    </div>
                                    <div className="info-item">
                                        <span className="info-label">🏁 End Date:</span>
                                        <span className="info-value">
                                            {new Date(bootcamp.endDate).toLocaleDateString()}
                                        </span>
                                    </div>
                                    <div className="info-item">
                                        <span className="info-label">👨‍🏫 Instructor ID:</span>
                                        <span className="info-value">{bootcamp.instructorId}</span>
                                    </div>
                                </div>
                                {user?.role === 'applicant' && bootcamp.bootcampState === 'Open' && (
                                    <button className="btn btn-primary btn-block">Apply Now</button>
                                )}
                            </div>
                        ))}
                    </div>
                )}
            </div>
        </div>
    );
};

export default Dashboard;
