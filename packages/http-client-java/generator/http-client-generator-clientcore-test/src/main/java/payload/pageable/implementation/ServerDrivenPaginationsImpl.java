// Code generated by Microsoft (R) TypeSpec Code Generator.

package payload.pageable.implementation;

import io.clientcore.core.annotation.ServiceInterface;
import io.clientcore.core.http.RestProxy;
import io.clientcore.core.http.annotation.HeaderParam;
import io.clientcore.core.http.annotation.HostParam;
import io.clientcore.core.http.annotation.HttpRequestInformation;
import io.clientcore.core.http.annotation.UnexpectedResponseExceptionDetail;
import io.clientcore.core.http.exception.HttpResponseException;
import io.clientcore.core.http.models.HttpMethod;
import io.clientcore.core.http.models.RequestOptions;
import io.clientcore.core.http.models.Response;
import payload.pageable.serverdrivenpagination.LinkResponse;

/**
 * An instance of this class provides access to all the operations defined in ServerDrivenPaginations.
 */
public final class ServerDrivenPaginationsImpl {
    /**
     * The proxy service used to perform REST calls.
     */
    private final ServerDrivenPaginationsService service;

    /**
     * The service client containing this operation class.
     */
    private final PageableClientImpl client;

    /**
     * Initializes an instance of ServerDrivenPaginationsImpl.
     * 
     * @param client the instance of the service client containing this operation class.
     */
    ServerDrivenPaginationsImpl(PageableClientImpl client) {
        this.service = RestProxy.create(ServerDrivenPaginationsService.class, client.getHttpPipeline());
        this.client = client;
    }

    /**
     * The interface defining all the services for PageableClientServerDrivenPaginations to be used by the proxy service
     * to perform REST calls.
     */
    @ServiceInterface(name = "PageableClientServer", host = "{endpoint}")
    public interface ServerDrivenPaginationsService {
        @HttpRequestInformation(
            method = HttpMethod.GET,
            path = "/payload/pageable/server-driven-pagination/link",
            expectedStatusCodes = { 200 })
        @UnexpectedResponseExceptionDetail
        Response<LinkResponse> linkSync(@HostParam("endpoint") String endpoint, @HeaderParam("Accept") String accept,
            RequestOptions requestOptions);
    }

    /**
     * The link operation.
     * <p><strong>Response Body Schema</strong></p>
     * 
     * <pre>
     * {@code
     * {
     *     pets (Required): [
     *          (Required){
     *             id: String (Required)
     *             name: String (Required)
     *         }
     *     ]
     *     next: String (Optional)
     * }
     * }
     * </pre>
     * 
     * @param requestOptions The options to configure the HTTP request before HTTP client sends it.
     * @throws HttpResponseException thrown if the service returns an error.
     * @return the response.
     */
    public Response<LinkResponse> linkWithResponse(RequestOptions requestOptions) {
        final String accept = "application/json";
        return service.linkSync(this.client.getEndpoint(), accept, requestOptions);
    }
}
