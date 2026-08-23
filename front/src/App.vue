<script setup lang="ts">
import { onMounted, onUnmounted, ref } from 'vue';
import * as signalR from '@microsoft/signalr';

  const messages = ref<{ id: any, content: string }[]>([]);
  const addedMessage = ref('');

  const connection = new signalR.HubConnectionBuilder()
    .withUrl('http://localhost:5164/hubs/chat')
    .withAutomaticReconnect()
    .build();

  async function loadMessages() {
    const response = await fetch('http://localhost:5164/chat');
    messages.value = await response.json();
  }

  async function addMessage() {
    if (!addedMessage.value.trim()) return;

    await fetch('http://localhost:5164/chat', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({
        content: addedMessage.value
      })
    })
  }

  onMounted(async () => {
    connection.on('messageReceived', (message: { id: string; content: string }) => {
      messages.value.push({ id: message.id, content: message.content });
    });

    await connection.start();

    await loadMessages();
  });

  onUnmounted(async () => {
    await connection.stop();
  });
</script>

<template>
  <main>
    <h1>mensagens!!!</h1>

    <form @submit.prevent="addMessage">
      <input
        v-model="addedMessage"
        placeholder="Mensagem"
      />

      <button type="submit">Adicionar</button>
    </form>

    <ul>
      <li
        v-for="message in messages"
        :key="message.id"
      >
        {{ message.id }} - {{ message.content }}
      </li>
    </ul>
  </main>
</template>

<style scoped></style>
