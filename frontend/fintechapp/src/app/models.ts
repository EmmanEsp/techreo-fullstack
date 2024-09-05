export interface CreateCustomerResponse {
    customerId: string;
}

export interface SigninResponse {
    customerId: string;
    name: string;
    lastName: string;
    email: string;
    phone: string;
    accountNumber: string;
    clabe: string;
    balance: number;
    token: string;
}

export interface TransactionResponse {
    type: string;
    amount: number;
    createdAt: string;
}

export interface ServiceResponse<T> {
    status: string;
    data: T;
}
