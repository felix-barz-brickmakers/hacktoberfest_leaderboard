import httpClient, { baseUrl } from '../httpClient';

describe('httpClient', () => {
  let fakeFetch: any;
  let fakeFetchMock: jest.Mock<any, any>;

  afterEach(() => {
    jest.restoreAllMocks();
    jest.resetAllMocks();
  });

  const fetchMockSuccess = (responseAsJson: Record<string, any> = { success: true }): void => {
    const response = {
      ok: true,
      json: () => Promise.resolve(responseAsJson),
    } as Response;
    fakeFetchMock = jest.fn();
    fakeFetch = fakeFetchMock.mockReturnValue(Promise.resolve(response));
    window.fetch = fakeFetch;
  };

  const fetchMockFail = (): void => {
    const response = {
      status: 400,
      statusText: 'error',
      ok: false,
      json: () => Promise.resolve({}),
    } as Response;
    fakeFetch = jest.fn().mockReturnValue(Promise.resolve(response));
    window.fetch = fakeFetch;
  };

  describe('post', () => {
    test('calls fetch with correct parameters and returns response', async () => {
      fetchMockSuccess();
      const payload = {
        myPayload: true,
      };

      const result = await httpClient.post('my-url', payload);

      expect(fakeFetch).toBeCalledWith(`${baseUrl}my-url`, {
        method: 'POST',
        headers: {
          Accept: 'application/json',
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(payload),
      });
      expect(result).toEqual({ success: true });
    });

    test('throws error when statusCode is not 200', async () => {
      fetchMockFail();

      await expect(httpClient.post('my-url', {})).rejects.toThrowError('error');
    });
  });

  describe('get', () => {
    test('calls fetch with correct parameters and returns response', async () => {
      fetchMockSuccess();

      const result = await httpClient.get('my-url2');

      expect(fakeFetch).toBeCalledWith(`${baseUrl}my-url2`, {
        method: 'GET',
        headers: {
          Accept: 'application/json',
          'Content-Type': 'application/json',
        },
      });
      expect(result).toEqual({ success: true });
    });

    test('throws error when statusCode is not 200', async () => {
      fetchMockFail();

      await expect(httpClient.get('my-url')).rejects.toThrowError('error');
    });
  });
});
