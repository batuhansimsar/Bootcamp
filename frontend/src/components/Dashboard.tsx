import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../AuthContext';
import { bootcampAPI, applicationAPI } from '../api';
import Toast from './Toast';
import './Dashboard.css';

interface Bootcamp {
    id: number;
    name: string;
    instructorId: number;
    startDate: string;
    endDate: string;
    bootcampState: number;
}

interface ToastState {
    show: boolean;
    message: string;
    type: 'success' | 'error' | 'info';
}

const Dashboard: React.FC = () => {
    const [bootcamps, setBootcamps] = useState<Bootcamp[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState('');
    const [appliedBootcamps, setAppliedBootcamps] = useState<Set<number>>(new Set());
    const [applyingTo, setApplyingTo] = useState<number | null>(null);
    const [toast, setToast] = useState<ToastState>({ show: false, message: '', type: 'info' });
    const { user, logout } = useAuth();
    const navigate = useNavigate();

    useEffect(() => {
        fetchBootcamps();
        if (user?.role === 'applicant') {
            checkAppliedBootcamps();
        }
    }, [user]);

    const fetchBootcamps = async () => {
        try {
            const response = await bootcampAPI.getAll();
            setBootcamps(response.data.items || response.data);
        } catch (err: any) {
            console.error(err);
            setError('Failed to load bootcamps');
        } finally {
            setLoading(false);
        }
    };

    const checkAppliedBootcamps = async () => {
        if (!user?.id) return;

        try {
            const applied = new Set<number>();
            for (const bootcamp of bootcamps) {
                const response = await applicationAPI.checkApplied(user.id, bootcamp.id);
                if (response.data.hasApplied) {
                    applied.add(bootcamp.id);
                }
            }
            setAppliedBootcamps(applied);
        } catch (err) {
            console.error('Failed to check applied bootcamps:', err);
        }
    };

    const handleApply = async (bootcampId: number) => {
        if (!user?.id) {
            showToast('Please login to apply', 'error');
            return;
        }

        setApplyingTo(bootcampId);
        try {
            await applicationAPI.create({
                applicantId: user.id,
                bootcampId: bootcampId
            });

            setAppliedBootcamps(prev => new Set(prev).add(bootcampId));
            showToast('Application submitted successfully! 🎉', 'success');
        } catch (err: any) {
            const errorMessage = err.response?.data?.message || 'Failed to submit application';
            showToast(errorMessage, 'error');
        } finally {
            setApplyingTo(null);
        }
    };

    const handleDelete = async (bootcampId: number) => {
        if (!confirm('Are you sure you want to delete this bootcamp?')) return;

        try {
            await bootcampAPI.delete(bootcampId);
            setBootcamps(prev => prev.filter(b => b.id !== bootcampId));
            showToast('Bootcamp deleted successfully', 'success');
        } catch (err: any) {
            showToast('Failed to delete bootcamp', 'error');
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
            case 0: return 'Preparing';
            case 1: return 'Open';
            case 2: return 'Started';
            case 3: return 'Completed';
            default: return 'Unknown';
        }
    };

    const getStatusBadge = (state: number) => {
        switch (state) {
            case 0: return 'badge-secondary';
            case 1: return 'badge-success';
            case 2: return 'badge-primary';
            case 3: return 'badge-warning';
            default: return 'badge-secondary';
        }
    };

    const canApply = (bootcamp: Bootcamp) => {
        return user?.role === 'applicant' &&
            bootcamp.bootcampState === 1 &&
            !appliedBootcamps.has(bootcamp.id);
    };

    const hasApplied = (bootcampId: number) => {
        return appliedBootcamps.has(bootcampId);
    };

    return (
        <div className="dashboard">
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
                    {user?.role === 'applicant' && (
                        <button
                            className="btn btn-outline"
                            onClick={() => navigate('/my-applications')}
                        >
                            My Applications
                        </button>
                    )}
                    {(user?.role === 'employee' || user?.role === 'instructor') && (
                        <>
                            <button
                                className="btn btn-primary"
                                onClick={() => navigate('/create-bootcamp')}
                            >
                                + Create Bootcamp
                            </button>
                            <button
                                className="btn btn-outline"
                                onClick={() => navigate('/manage-applications')}
                            >
                                Manage Applications
                            </button>
                        </>
                    )}
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
                                        {getStatusLabel(bootcamp.bootcampState)}
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

                                <div className="bootcamp-actions">
                                    {canApply(bootcamp) && (
                                        <button
                                            className="btn btn-primary btn-block"
                                            onClick={() => handleApply(bootcamp.id)}
                                            disabled={applyingTo === bootcamp.id}
                                        >
                                            {applyingTo === bootcamp.id ? (
                                                <div className="spinner-small"></div>
                                            ) : (
                                                'Apply Now'
                                            )}
                                        </button>
                                    )}

                                    {hasApplied(bootcamp.id) && user?.role === 'applicant' && (
                                        <button className="btn btn-success btn-block" disabled>
                                            ✓ Applied
                                        </button>
                                    )}

                                    {(user?.role === 'employee' || user?.role === 'instructor') && (
                                        <div className="admin-actions">
                                            <button
                                                className="btn btn-outline"
                                                onClick={() => navigate(`/edit-bootcamp/${bootcamp.id}`)}
                                            >
                                                Edit
                                            </button>
                                            <button
                                                className="btn btn-danger"
                                                onClick={() => handleDelete(bootcamp.id)}
                                            >
                                                Delete
                                            </button>
                                        </div>
                                    )}
                                </div>
                            </div>
                        ))}
                    </div>
                )}
            </div>
        </div>
    );
};

export default Dashboard;
