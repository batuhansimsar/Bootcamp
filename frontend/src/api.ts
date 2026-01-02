import axios from 'axios';

const API_BASE_URL = 'http://localhost:5158/api';

const api = axios.create({
    baseURL: API_BASE_URL,
});

// Add auth token to requests
api.interceptors.request.use((config) => {
    const token = localStorage.getItem('token');
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
});

// Auth API
export const authAPI = {
    login: (email: string, password: string) =>
        api.post('/Auth/login', { email, password }),

    registerApplicant: (data: any) => api.post('/Auth/register/applicant', data),

    registerInstructor: (data: {
        firstName: string;
        lastName: string;
        dateOfBirth: string;
        nationalityIdentity: string;
        email: string;
        password: string;
        companyName: string;
    }) => api.post('/Auth/register/instructor', data),

    registerEmployee: (data: {
        firstName: string;
        lastName: string;
        dateOfBirth: string;
        nationalityIdentity: string;
        email: string;
        password: string;
        position: string;
    }) => api.post('/Auth/register/employee', data),
};

// Bootcamp API
export const bootcampAPI = {
    getAll: () => api.get('/Bootcamps'),
    getById: (id: number) => api.get(`/Bootcamps/${id}`),
    create: (data: {
        name: string;
        instructorId: number;
        startDate: string;
        endDate: string;
    }) => api.post('/Bootcamps', data),
    update: (id: number, data: any) => api.put(`/Bootcamps/${id}`, data),
    delete: (id: number) => api.delete(`/Bootcamps/${id}`),
};

// Application API
export const applicationAPI = {
    getAll: () => api.get('/Applications'),
    getById: (id: number) => api.get(`/Applications/${id}`),
    create: (data: {
        applicantId: number;
        bootcampId: number;
    }) => api.post('/Applications', data),
    update: (id: number, data: any) => api.put(`/Applications/${id}`, data),
    delete: (id: number) => api.delete(`/Applications/${id}`),
    getMyApplications: (applicantId: number) => api.get(`/Applications/my-applications/${applicantId}`),
    getByBootcamp: (bootcampId: number) => api.get(`/Applications/bootcamp/${bootcampId}`),
    checkApplied: (applicantId: number, bootcampId: number) =>
        api.get(`/Applications/check?applicantId=${applicantId}&bootcampId=${bootcampId}`),
    updateStatus: (data: {
        applicationId: number;
        newState: number;
    }) => api.patch('/Applications/status', data),
};

// Blacklist API
export const blacklistAPI = {
    getAll: () => api.get('/Blacklists'),
    add: (data: {
        applicantId: number;
        reason: string;
    }) => api.post('/Blacklists', data),
    remove: (id: number) => api.delete(`/Blacklists/${id}`),
};

// Applicant API
export const applicantAPI = {
    getAll: () => api.get('/Applicants'),
    getById: (id: number) => api.get(`/Applicants/${id}`),
};

// Instructor API
export const instructorAPI = {
    getAll: () => api.get('/Instructors'),
    getById: (id: number) => api.get(`/Instructors/${id}`),
};

export default api;
