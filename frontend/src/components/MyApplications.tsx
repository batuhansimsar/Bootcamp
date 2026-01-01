import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../AuthContext';
import { applicationAPI } from '../api';
import Toast from './Toast';
import './MyApplications.css';

interface Application {
    id: number;
    applicantId: number;
    bootcampId: number;
    applicationState: number;
    bootcamp: {
        id: number;
        name: string;
        startDate: string;
        endDate: string;
        bootcampState: number;
    };
}

interface ToastState {
    show: boolean;
    message: string;
    type: 'success' | 'error' | 'info';
}

const MyApplications: React.FC = () => {
    const [applications, setApplications] = useState<Application[]>([]);
    const [loading, setLoading] = useState(true);
    const [toast, setToast] = useState<ToastState>({ show: false, message: '', type: 'info' });
    const { user, logout } = useAuth();
    const navigate = useNavigate();

    useEffect(() => {
        if (user?.id) {
            fetchMyApplications();
        }
    }, [user]);

    const fetchMyApplications = async () => {
        if (!user?.id) return;

        try {
            const response = await applicationAPI.getMyApplications(user.id);
            setApplications(response.data);
        } catch (err: any) {
            console.error('Failed to load applications:', err);
            showToast('Failed to load applications', 'error');
        } finally {
            setLoading(false);
        }
    };

    const handleCancelApplication = async (applicationId: number) => {
        if (!confirm('Are you sure you want to cancel this application?')) return;

        try {
            await applicationAPI.delete(applicationId);
            setApplications(prev => prev.filter(app => app.id !== applicationId));
            showToast('Application cancelled successfully', 'success');
        } catch (err: any) {
            showToast('Failed to cancel application', 'error');
        }
    };

    const showToast = (message: string, type: 'success' | 'error' | 'info') => {
        setToast({ show: true, message, type });
    };

    const handleLogout = () => {
        logout();
        navigate('/login');
    };

    const getStatusLabel = (state: number) => {
        switch (state) {
            case 0: return 'Pending';
            case 1: return 'Approved';
            case 2: return 'Rejected';
            default: return 'Unknown';
        }
    };

    const getStatusBadge = (state: number) => {
        switch (state) {
            case 0: return 'badge-warning';
            case 1: return 'badge-success';
            case 2: return 'badge-danger';
            default: return 'badge-secondary';
        }
    };

    const getBootcampStatusLabel = (state: number) => {
        switch (state) {
            case 0: return 'Preparing';
            case 1: return 'Open';
            case 2: return 'Started';
            case 3: return 'Completed';
            default: return 'Unknown';
        }
    };

    return (
        <div className="my-applications">
            {toast.show && (
                <Toast
                    message={toast.message}
                    type={toast.type}
                    onClose={() => setToast({ ...toast, show: false })}
                />
            )}

            <nav className="dashboard-nav glass">
                <div className="nav-brand">
                    <h2>🚀 Bootcamp</h2>
                </div>
                <div className="nav-menu">
                    <button
                        className="btn btn-outline"
                        onClick={() => navigate('/dashboard')}
                    >
                        ← Back to Dashboard
                    </button>
                    <span className="user-info">
                        👨‍🎓 {user?.email}
                    </span>
                    <button className="btn btn-secondary" onClick={handleLogout}>
                        Logout
                    </button>
                </div>
            </nav>

            <div className="applications-content container">
                <div className="applications-header">
                    <h1 className="fade-in">My Applications</h1>
                    <p className="fade-in">Track your bootcamp applications</p>
                </div>

                {loading ? (
                    <div className="loading-state">
                        <div className="spinner"></div>
                        <p>Loading applications...</p>
                    </div>
                ) : applications.length === 0 ? (
                    <div className="empty-state card">
                        <h3>No applications yet</h3>
                        <p>You haven't applied to any bootcamps yet</p>
                        <button
                            className="btn btn-primary"
                            onClick={() => navigate('/dashboard')}
                        >
                            Browse Bootcamps
                        </button>
                    </div>
                ) : (
                    <div className="applications-list">
                        {applications.map((application) => (
                            <div key={application.id} className="application-card card">
                                <div className="application-header">
                                    <div>
                                        <h3>{application.bootcamp.name}</h3>
                                        <span className={`badge ${getStatusBadge(application.applicationState)}`}>
                                            {getStatusLabel(application.applicationState)}
                                        </span>
                                    </div>
                                    <span className="bootcamp-status">
                                        {getBootcampStatusLabel(application.bootcamp.bootcampState)}
                                    </span>
                                </div>

                                <div className="application-info">
                                    <div className="info-item">
                                        <span className="info-label">📅 Start Date:</span>
                                        <span className="info-value">
                                            {new Date(application.bootcamp.startDate).toLocaleDateString()}
                                        </span>
                                    </div>
                                    <div className="info-item">
                                        <span className="info-label">🏁 End Date:</span>
                                        <span className="info-value">
                                            {new Date(application.bootcamp.endDate).toLocaleDateString()}
                                        </span>
                                    </div>
                                    <div className="info-item">
                                        <span className="info-label">📋 Application ID:</span>
                                        <span className="info-value">#{application.id}</span>
                                    </div>
                                </div>

                                {application.applicationState === 0 && (
                                    <button
                                        className="btn btn-danger btn-block"
                                        onClick={() => handleCancelApplication(application.id)}
                                    >
                                        Cancel Application
                                    </button>
                                )}
                            </div>
                        ))}
                    </div>
                )}
            </div>
        </div>
    );
};

export default MyApplications;
