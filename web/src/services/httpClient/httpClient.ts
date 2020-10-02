export const baseUrl = process.env.VUE_APP_ENV === 'production' ? window.location.origin : 'https://localhost:5001/';

enum HttpMethod {
  POST = 'POST',
  GET = 'GET'
}

const send = async <T>(
  url: string,
  method: HttpMethod,
  payload: Record<string, unknown> | null,
): Promise<T> => {
  const headers: Record<string, string> = {
    Accept: 'application/json',
    'Content-Type': 'application/json',
  };

  const options: RequestInit = {
    method,
    headers,
  };
  if (payload) {
    options.body = JSON.stringify(payload);
  }

  const response = await fetch(baseUrl + url, options);

  if (!response.ok) {
    throw new Error(response.statusText);
  }
  return response.json() as Promise<T>;
};

const httpClient = {
  async get<T>(url: string): Promise<T> {
    return send(url, HttpMethod.GET, null);
  },
  async post<T>(url: string, payload: Record<string, unknown>): Promise<T> {
    return send(url, HttpMethod.POST, payload);
  },
};

export default httpClient;
