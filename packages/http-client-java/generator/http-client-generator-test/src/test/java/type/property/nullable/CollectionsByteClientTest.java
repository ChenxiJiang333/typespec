// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

package type.property.nullable;

import static org.junit.jupiter.api.Assertions.assertNotNull;
import static org.junit.jupiter.api.Assertions.assertNull;

import java.util.Arrays;
import org.junit.jupiter.api.Test;
import type.property.nullable.models.CollectionsByteProperty;

class CollectionsByteClientTest {

    CollectionsByteClient client = new NullableClientBuilder().buildCollectionsByteClient();

    @Test
    void patchNonNullWithResponse() {
        CollectionsByteProperty collectionsByteProperty = new CollectionsByteProperty().setRequiredProperty("foo")
            .setNullableProperty(
                Arrays.asList(new byte[] { 104, 101, 108, 108, 111, 44, 32, 119, 111, 114, 108, 100, 33 },
                    new byte[] { 104, 101, 108, 108, 111, 44, 32, 119, 111, 114, 108, 100, 33 }));
        client.patchNonNull(collectionsByteProperty);
    }

    @Test
    void patchNullWithResponse() {
        client.patchNull(new CollectionsByteProperty().setRequiredProperty("foo").setNullableProperty(null));
    }

    @Test
    void getNonNull() {
        CollectionsByteProperty response = client.getNonNull();
        assertNotNull(response.getNullableProperty());
    }

    @Test
    void getNull() {
        CollectionsByteProperty response = client.getNull();
        assertNull(response.getNullableProperty());
    }
}
