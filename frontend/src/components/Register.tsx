import React, { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { authAPI } from '../api';
import './Register.css';

type UserRole = 'applicant' | 'instructor' | 'employee';

const Register: React.FC = () => {
    const [role, setRole] = useState<UserRole>('applicant');
    const [formData, setFormData] = useState({
        firstName: '',
        lastName: '',
        email: '',
        password: '',
        dateOfBirth: '',
        nationalityIdentity: '',
        about: '',
        companyName: '',
        position: '',
    });
    const [error, setError] = useState('');
    const [success, setSuccess] = useState(false);
    const [loading, setLoading] = useState(false);
    const navigate = useNavigate();

    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setError('');
        setLoading(true);

        try {
            const baseData = {
                firstName: formData.firstName,
                lastName: formData.lastName,
                email: formData.email,
                password: formData.password,
            };

            if (role === 'applicant') {
                await authAPI.registerApplicant({
                    ...baseData,
                    dateOfBirth: formData.dateOfBirth,
                    nationalityIdentity: formData.nationalityIdentity,
                    about: formData.about,
                });
            } else if (role === 'instructor') {
                await authAPI.registerInstructor({
                    ...baseData,
                    companyName: formData.companyName,
                });
            } else {
                await authAPI.registerEmployee({
                    ...baseData,
                    position: formData.position,
                });
            }

            setSuccess(true);
            setTimeout(() => navigate('/login'), 2000);
        } catch (err: any) {
            setError(err.response?.data?.message || 'Registration failed. Please try again.');
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="register-container">
            <div className="register-card glass">
                <div className="register-header">
                    <h1>🚀 Join Bootcamp</h1>
                    <p>Create your account</p>
                </div>

                {error && <div className="alert alert-error">{error}</div>}
                {success && <div className="alert alert-success">Registration successful! Redirecting to login...</div>}

                <div className="role-selector">
                    <button
                        type="button"
                        className={`role-btn ${role === 'applicant' ? 'active' : ''}`}
                        onClick={() => setRole('applicant')}
                    >
                        👨‍🎓 Applicant
                    </button>
                    <button
                        type="button"
                        className={`role-btn ${role === 'instructor' ? 'active' : ''}`}
                        onClick={() => setRole('instructor')}
                    >
                        👨‍🏫 Instructor
                    </button>
                    <button
                        type="button"
                        className={`role-btn ${role === 'employee' ? 'active' : ''}`}
                        onClick={() => setRole('employee')}
                    >
                        👔 Employee
                    </button>
                </div>

                <form onSubmit={handleSubmit} className="register-form">
                    <div className="form-row">
                        <div className="form-group">
                            <label className="form-label">First Name</label>
                            <input
                                type="text"
                                name="firstName"
                                value={formData.firstName}
                                onChange={handleChange}
                                required
                            />
                        </div>
                        <div className="form-group">
                            <label className="form-label">Last Name</label>
                            <input
                                type="text"
                                name="lastName"
                                value={formData.lastName}
                                onChange={handleChange}
                                required
                            />
                        </div>
                    </div>

                    <div className="form-group">
                        <label className="form-label">Email</label>
                        <input
                            type="email"
                            name="email"
                            value={formData.email}
                            onChange={handleChange}
                            required
                        />
                    </div>

                    <div className="form-group">
                        <label className="form-label">Password</label>
                        <input
                            type="password"
                            name="password"
                            value={formData.password}
                            onChange={handleChange}
                            required
                        />
                    </div>

                    {role === 'applicant' && (
                        <>
                            <div className="form-row">
                                <div className="form-group">
                                    <label className="form-label">Date of Birth</label>
                                    <input
                                        type="date"
                                        name="dateOfBirth"
                                        value={formData.dateOfBirth}
                                        onChange={handleChange}
                                        required
                                    />
                                </div>
                                <div className="form-group">
                                    <label className="form-label">National ID</label>
                                    <input
                                        type="text"
                                        name="nationalityIdentity"
                                        value={formData.nationalityIdentity}
                                        onChange={handleChange}
                                        required
                                    />
                                </div>
                            </div>
                            <div className="form-group">
                                <label className="form-label">About</label>
                                <textarea
                                    name="about"
                                    value={formData.about}
                                    onChange={handleChange}
                                    rows={3}
                                    required
                                />
                            </div>
                        </>
                    )}

                    {role === 'instructor' && (
                        <div className="form-group">
                            <label className="form-label">Company Name</label>
                            <input
                                type="text"
                                name="companyName"
                                value={formData.companyName}
                                onChange={handleChange}
                                required
                            />
                        </div>
                    )}

                    {role === 'employee' && (
                        <div className="form-group">
                            <label className="form-label">Position</label>
                            <input
                                type="text"
                                name="position"
                                value={formData.position}
                                onChange={handleChange}
                                required
                            />
                        </div>
                    )}

                    <button type="submit" className="btn btn-primary btn-block" disabled={loading}>
                        {loading ? <div className="spinner-small"></div> : 'Create Account'}
                    </button>
                </form>

                <div className="register-footer">
                    <p>Already have an account? <Link to="/login">Sign in</Link></p>
                </div>
            </div>
        </div>
    );
};

export default Register;
