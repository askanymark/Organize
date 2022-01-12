organizedIndexedDb = {
    db: {},
    initAsync: async () => {
        const open = new Promise((resolve, reject) => {
            const request = window.indexedDB.open('organize');

            request.onupgradeneeded = this.onUpgradeNeeded;
            request.onsuccess = event => resolve(event.target.result);
            request.onerror = event => reject(event);
        });

        this.db = await open;
    },
    onUpgradeNeeded: event => {
        const db = event.target.result;
        db.createObjectStore('User', {keyPath: 'id', autoIncrement: true});

        const textItemStore = db.createObjectStore('TextItem', {keyPath: 'id', autoIncrement: true});
        textItemStore.createIndex('parentId', 'parentId', {unique: false});

        const urlItemStore = db.createObjectStore('UrlItem', {keyPath: 'id', autoIncrement: true});
        urlItemStore.createIndex('parentId', 'parentId', {unique: false});

        const parentItemStore = db.createObjectStore('ParentItem', {keyPath: 'id', autoIncrement: true});
        parentItemStore.createIndex('parentId', 'parentId', {unique: false});

        const childItemStore = db.createObjectStore('ChildItem', {keyPath: 'id', autoIncrement: true});
        childItemStore.createIndex('parentId', 'parentId', {unique: false});
    },
    getAllAsync: async (tableName) => {
        return await new Promise((resolve, reject) => {
            const transaction = this.db.transaction(tableName);
            transaction.onerror = event => reject(event);

            const store = transaction.objectStore(tableName);
            const elements = [];
            store.openCursor().onsuccess = event => {
                let cursor = event.target.result;
                if (cursor) {
                    elements.push(cursor.value);
                    cursor.continue();
                } else {
                    resolve(elements);
                }
            };
        });
    },
    addAsync: async (tableName, entityToAdd) => {
        console.log(entityToAdd);
        entityToAdd = JSON.parse(entityToAdd);
        console.log(entityToAdd);
        delete entityToAdd.id;

        return await new Promise((resolve, reject) => {
            const transaction = this.db.transaction(tableName, 'readwrite');
            transaction.onerror = event => reject(event);
            const store = transaction.objectStore(tableName);
            const request = store.add(entityToAdd);
            request.onsuccess = event => {
                resolve(event.target.result);
            };
        });
    },
    putAsync: async (tableName, entityToPut, id) => {
        entityToPut = JSON.parse(entityToPut);

        return await new Promise((resolve, reject) => {
            const transaction = this.db.transaction(tableName, 'readwrite');
            transaction.onerror = event => reject(event);
            const store = transaction.createObjectStore(tableName)
            entityToPut.id = id;
            const request = store.put(entityToPut);
            request.onsuccess = () => resolve();
        });
    },
    deleteAsync: async (tableName, id) => {
        return await new Promise((resolve, reject) => {
            const transaction = this.db.transaction(tableName, 'readwrite');
            transaction.onerror = event => reject(event);
            const store = transaction.createObjectStore(tableName);
            const request = store.delete(id);
            request.onsuccess = () => resolve();
        });
    }
}